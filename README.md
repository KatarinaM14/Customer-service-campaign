# Customer Service Campaign Project

This project simulates a real-world scenario for a large telecommunication company running a customer loyalty campaign. The project consists of two ASP.NET Core APIs and a React frontend:

1. **Telecommunication Company API**: Used by agents to reward loyal customers with discounts.
2. **Integration API**: Merges reward data with customer data and exposes the results through an API.
3. **Frontend (React)**: Allows agents to interact with the Telecommunication Company API.

## Project Architecture

- **Telecommunication Company API**:
  - **Purpose**: Provides endpoints for agents to reward customers.
  - **Technologies**: ASP.NET Core, Entity Framework Core, JWT Authentication.
  - **Key Features**:
    - Customer reward management.
    - Secure authentication and authorization.

- **Integration API**:
  - **Purpose**: Merges customer data with reward data from a .csv report.
  - **Technologies**: ASP.NET Core, Entity Framework Core, API Keys, CSV processing library.
  - **Key Features**:
    - Data merging and reporting.
    - Securely exposed API for integration with other CRM solutions.

- **Frontend (React)**:
  - **Purpose**: Provides a user interface for agents to interact with the Telecommunication Company API.
  - **Technologies**: React, Axios for API calls.

- **Database**:
  - **MSSQL**: Both APIs use MSSQL as the database, ensuring secure and reliable data storage and retrieval.
