# PsychoNeuroAssessment

**PsychoNeuroAssessment** is an advanced, microservices-based neurocognitive assessment platform designed to deliver cutting-edge psychological testing, analysis, and donation tracking. Built with a focus on scalability, security, and innovation, this project showcases my expertise in **full-stack development**, **AI/ML integration**, **statistical analysis**, and **modern DevOps practices**, positioning it as a standout portfolio piece for my relocation to the EU job market.

## Project Overview
**PsychoNeuroAssessment** leverages a robust architecture to provide a comprehensive suite of features:
- **Microservices Architecture**: Powered by ASP.NET Core 8.0, orchestrated via Docker Compose and Kubernetes, with Ocelot API Gateway routing across services like `AuthService`, `TestService`, `DonationService`, and `AnalysisService`.
- **AI and Machine Learning**: Integrated ML.NET regression and classification models to predict test outcomes, enhanced with SHAP explainability for transparency in psychological insights.
- **Statistical Analysis**: Offers descriptive (mean, median, SD) and inferential (t-tests, ANOVA) statistics, plus advanced factor analysis (PCA, EFA, CFA) using MathNet.Numerics for psychometric rigor.
- **Questionnaire System**: Dynamic test creation and scoring, supporting validated psychological assessments stored in MongoDB.
- **User and Admin UI**: Responsive Blazor WebAssembly and Server apps with real-time SignalR updates, ensuring an accessible, WCAG-compliant experience.
- **Security**: Implements JWT and OAuth2 authentication with role-based access (User, Admin, Analyst), backed by audit logging.
- **Blockchain Integration**: Features an ERC-20 smart contract on Ethereum for transparent donation tracking, integrated via NEthereum.
- **Monitoring**: Prometheus and Grafana dashboards with OpenTelemetry tracing for observability, plus alerting for system health.
- **Scalability**: Redis caching, RabbitMQ message queues, and Ocelot load balancing ensure performance under load.
- **CI/CD**: Automated GitHub Actions pipeline for building, testing, and deploying Docker images to multi-environment setups (Dev, Prod).

## My Contributions
As the sole architect and developer, I:
- Designed and implemented a **CQRS-based microservices ecosystem** adhering to SOLID principles and Clean Code standards.
- Engineered **AI-driven predictive analytics** and **statistical models** to deliver actionable psychological insights.
- Developed a **secure, scalable infrastructure** with Docker, Kubernetes, and modern monitoring tools.
- Created an **intuitive Blazor UI** to enhance user engagement and admin control.
- Integrated **blockchain technology**, showcasing innovation in donation transparency.

## Technologies Used
- **Backend**: C#, ASP.NET Core 8.0, MediatR, Ocelot, NEthereum
- **Frontend**: Blazor WASM/Server, SignalR
- **Data**: MongoDB, Redis, MathNet.Numerics, ML.NET
- **DevOps**: Docker, Kubernetes, Prometheus, Grafana, GitHub Actions, OpenTelemetry
- **Blockchain**: Solidity, Ethereum
- **Testing**: xUnit, Moq


---

**Setup Instructions** (To be added later as we complete the solution): Clone the repo, run `docker-compose up -d`, and access at `http://localhost:5000`.
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
