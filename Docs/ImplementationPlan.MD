# 🎥 WatchVault – General Implementation Plan

> This is a first, very general plan of the app, intended for stakeholders to understand the overall concept and workflow.  

## Business Idea
A web app where users can explore movies (from an external API), save them to a personal list, and track watched movies with ratings/reviews.

## Technology Stack
- **Backend:** .NET 8 Web API  
- **Frontend:** Angular 20  
- **Database:** PostgreSQL  
- **Containerization:** Docker  

## Authentication (JWT)
- POST /api/auth/register – register new user  
- POST /api/auth/login – login, returns JWT  
- GET /api/auth/me – get current user  

## Backend Models
- User (Id, Username, Email, PasswordHash, PasswordSalt)  
- Movie (Id, ExternalId, Title, Year, PosterUrl, CreatedAt)  
- UserMovie (Id, UserId, MovieId, Status [ToWatch | Watched], WatchedDate, Review, Rating)  

## Movie Endpoints
- GET /api/movies/search?query=xyz – proxy search to external API  
- POST /api/user-movies – add movie to user’s list (status=ToWatch or Watched)  
- GET /api/user-movies – get user’s movies (filter by status)  
- GET /api/user-movies/{id} – get specific user-movie  
- PUT /api/user-movies/{id} – update status/review/rating/watchedDate  
- DELETE /api/user-movies/{id} – remove from user’s list  

## Frontend Views & Components
- Auth: LoginComponent, RegisterComponent  
- Movies: MovieSearchComponent, MovieDetailsComponent  
- User Lists: WatchlistComponent, WatchedListComponent, ReviewFormComponent  
- Shared: NavbarComponent, AuthGuardService  

## Database
- Users (Id, Username, Email, PasswordHash, PasswordSalt)  
- Movies (Id, ExternalId, Title, Year, PosterUrl)  
- UserMovies (Id, UserId, MovieId, Status, WatchedDate, ReviewId)  
- Review (Id, ReviewDate, ReviewText, Rate)  

## App Flow Example
1. User registers and logs in (JWT stored).  
2. Searches for a movie via external API.  
3. Adds it to Watchlist → saved in DB.  
4. Later marks it as Watched → updates status, adds review.  
5. Can browse Watchlist or Watched History.  
