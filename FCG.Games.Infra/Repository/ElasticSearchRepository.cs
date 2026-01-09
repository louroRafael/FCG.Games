using Elastic.Clients.Elasticsearch;
using FCG.Games.Domain.DTOs.ElasticDocuments;
using FCG.Games.Domain.Interfaces.Repositories;

namespace FCG.Games.Infra.Repository;

public class ElasticSearchRepository(ElasticsearchClient client) : IElasticSearchRepository
{
    private readonly IndexName _index = "fcg-games";

    public async Task IndexAsync(GameElasticDocument game, CancellationToken ct)
    {
        var response = await client.IndexAsync(game, i => i
            .Index(_index)
            .Id(game.Id)
            .Refresh(Refresh.True),
            ct
        );

        if (!response.IsValidResponse)
        {
            var reason = response.ElasticsearchServerError?.Error.Reason
                         ?? "Unknown Elasticsearch error";

            throw new InvalidOperationException(
                $"Failed to index game {game.Id} in Elasticsearch. Reason: {reason}"
            );
        }
    }

    public async Task<IReadOnlyCollection<GameElasticDocument>> SearchAsync(string? query, string? genre, string? platform, CancellationToken ct)
    {
        var response = await client.SearchAsync<GameElasticDocument>(s => s
            .Indices(_index)
            .Size(20)
            .Query(q => q.Bool(b =>
            {
                if (!string.IsNullOrWhiteSpace(query))
                {
                    b.Must(m => m.MultiMatch(mm => mm
                        .Query(query)
                        .Fields(new[] { "title^3", "description^2", "developer" })
                    ));
                }

                if (!string.IsNullOrWhiteSpace(genre))
                {
                    b.Filter(f => f
                        .Term(t => t
                            .Field(p => p.Genre.Suffix("keyword"))
                            .Value(genre)
                        )
                    );
                }

                if (!string.IsNullOrWhiteSpace(platform))
                {
                    b.Filter(f => f
                        .Term(t => t
                            .Field(p => p.Platform.Suffix("keyword"))
                            .Value(platform)
                        )
                    );
                }
            })),
            ct
        );

        if (!response.IsValidResponse)
            throw new InvalidOperationException(
                response.ElasticsearchServerError?.Error.Reason
                ?? "Elasticsearch search failed"
            );

        return response.Documents;
    }

    public async Task<GameMetricsResult> GetMetricsAsync(CancellationToken ct)
    {
        var response = await client.SearchAsync<GameElasticDocument>(s => s
            .Indices(_index)
            .Size(0)
            .Aggregations(aggs => aggs
                .Add("games_by_genre", a => a
                    .Terms(t => t
                        .Field(f => f.Genre.Suffix("keyword"))
                        .Size(50)
                    )
                )
                .Add("games_by_platform", a => a
                    .Terms(t => t
                        .Field(f => f.Platform.Suffix("keyword"))
                        .Size(50)
                    )
                )
                .Add("price_stats", a => a
                    .Stats(st => st
                        .Field(f => f.Price)
                    )
                )
            ),
            ct
        );

        var result = new GameMetricsResult();

        var gamesByGenre = response.Aggregations.GetStringTerms("games_by_genre");
        if (gamesByGenre != null)
        {
            foreach (var bucket in gamesByGenre.Buckets)
            {
                result.GamesByGenre[bucket.Key.ToString()] = bucket.DocCount;
            }
        }

        var gamesByPlatform = response.Aggregations.GetStringTerms("games_by_platform");
        if (gamesByPlatform != null)
        {
            foreach (var bucket in gamesByPlatform.Buckets)
            {
                result.GamesByPlatform[bucket.Key.ToString()] = bucket.DocCount;
            }
        }

        var priceStats = response.Aggregations.GetStats("price_stats");
        if (priceStats != null)
        {
            result.AvgPrice = (decimal?)priceStats?.Avg ?? 0;
            result.MinPrice = (decimal?)priceStats?.Min ?? 0;
            result.MaxPrice = (decimal?)priceStats?.Max ?? 0;
        }

        return result;
    }

    public async Task<IReadOnlyCollection<GameElasticDocument>> GetRecommendationsAsync(string genre, string platform, string gameId, CancellationToken ct)
    {
        var response = await client.SearchAsync<GameElasticDocument>(s => s
            .Indices(_index)
            .Size(10)
            .Query(q => q
                .Bool(b => b
                    .Must(
                        m => m.Term(t => t
                            .Field(f => f.Genre.Suffix("keyword"))
                            .Value(genre)
                        ),
                        m => m.Term(t => t
                            .Field(f => f.Platform.Suffix("keyword"))
                            .Value(platform)
                        )
                    )
                    .MustNot(
                        mn => mn.Term(t => t
                            .Field(f => f.Id.Suffix("keyword"))
                            .Value(gameId)
                        )
                    )
                )
            )
        );

        if (!response.IsValidResponse)
            throw new InvalidOperationException(
                response.ElasticsearchServerError?.Error.Reason
                ?? "Elasticsearch search failed"
            );

        return response.Documents.ToList();
    }
}
