# Flight Booking API

A sample .NET Web API for flight booking, demonstrating Agent Skills integration.

## Features

- Search flights by origin, destination, and date
- Book flights with passenger details
- Cancel bookings
- View booking status

## API Endpoints

### Flights
- `GET /api/flights` - List all flights
- `GET /api/flights/{id}` - Get flight details
- `POST /api/flights/search` - Search flights

### Bookings
- `GET /api/bookings` - List all bookings
- `GET /api/bookings/{reference}` - Get booking by reference
- `POST /api/bookings` - Create booking
- `DELETE /api/bookings/{reference}` - Cancel booking

## Running the API

```bash
cd flight-booking
dotnet run
```

API will be available at: https://localhost:5001/swagger

## Agent Skills

This project uses centralized Agent Skills for development workflows.

Skills are located in `.github/skills/` (git submodule):
- **work-on-ticket**: Jira integration for ticket management
- **code-review**: Automated code review checklist
- **api-integration**: REST API best practices
- **data-analysis**: Data analysis utilities

## Jira Configuration

Create `.jira-config` in project root:

```ini
[DEFAULT]
default_profile = ihkreddy

[ihkreddy]
url = https://ihkreddy.atlassian.net
email = your-email@example.com
token = your-api-token
```
