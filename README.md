# PsychoNeuroAssessment

A microservices-based application using .NET 8, adhering to SOLID principles, Clean Code, and CQRS.

## Setup
1. Install .NET 8 SDK.
2. Run `docker-compose up --build` to start all services.
3. Access:
   - Auth API: http://localhost:5001/swagger
   - Test API: http://localhost:5002/swagger
   - Donation API: http://localhost:5003/swagger
   - Analysis API: http://localhost:5004/swagger
   - API Gateway: http://localhost:5000
   - Blazor WASM: http://localhost:5005
   - Blazor Server: http://localhost:5006
   - Prometheus: http://localhost:9090
   - Seq: http://localhost:5341