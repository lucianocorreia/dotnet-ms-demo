# start auctions service
# @dotnet watch run --non-interactive --project src/AuctionService
auction-service: 
	@dotnet watch run --project src/AuctionService

# start search service
# @dotnet watch run --non-interactive --project src/SearchService
search-service: 
	@dotnet watch run --project src/SearchService

.PHONY: auction_sservice, search-servicear
