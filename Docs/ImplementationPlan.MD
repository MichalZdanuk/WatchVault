# 🎥 WatchVault – General Implementation Plan

> This is a first, very general plan of the app, intended for stakeholders to understand the overall concept and workflow.  

## Business Idea
A web app where users can explore movies (from an external API), save them to a personal list, and track watched movies with ratings/reviews.

## Technology Stack
- **Backend:** .NET 8 Web API
- **Frontend:** Angular 20
- **Database:** PostgreSQL
- **Cache:** Redis
- **Identity Provider:** Keycloak
- **Simkl API:** external API
- **Containerization:** Docker

## Authentication (JWT)
- POST /api/auth/register – register new user
- POST /api/auth/retrieve-token – returns JWT  
- GET /api/auth/me – get current user

## Backend Models
- User (Username, Email, FirstName, LastName)  
- WatchList (UserId, TotalWatched, TotalToWatch, WatchListItems)
- WatchListItem (WatchStatus, AddedToWatchAt, WatchedAt, Movie)
- Movie (SimklId, Title, Year, PosterUrl, ReleaseDate, RuntimeMinutes, Director, Overview)

## Movie Endpoints
- GET /api/simkl/movies/search?query=xyz – browse movies (proxy search to external API)
- GET /api/simkl/movies/id - get movie details (proxy search to external API)
- GET /api/simkl/movies/trending?trendingInterval=month - get trending movies (proxy search to external API)

## WatchList Endpoints
- POST /api/watchlist/add - add movie to watchlist (for authenticated user)
- GET /api/watchlist - get watchlist with items (for authenticated user)
- DELETE /api/watchlist/items/id - remove specified item from watchlist (for authenticated user)

## Frontend Views & Components
- Auth: LoginComponent, RegisterComponent
- Movies: MovieSearchComponent, MovieDetailsComponent
- User Lists: WatchlistComponent, WatchedListComponent, ReviewFormComponent
- Shared: NavbarComponent, AuthGuardService

## Database
- Users
- WatchLists
- WatchListItems 

## App Flow Example
1. User registers and logs in (JWT stored).
2. Searches for a movie via external API.
3. Adds it to Watchlist → saved in DB.
4. Later marks it as Watched → updates status, adds review.
5. Can browse Watchlist or Watched History.