# Contributing to GoodFood

Thanks for your interest in contributing! 🙌

## Getting Started

1. Fork the repository and clone your fork.
2. Create a branch from `master`:
   - `git checkout -b feature/<short-description>`
3. Set up the environment (see README for prerequisites) and run tests:
   - `dotnet test tests/GoodFood.Tests`

## Development Guidelines

- Coding style enforced via analyzers configured in `Directory.Build.props`.
- Use domain types in business logic; map to DTOs at boundaries.
- Keep application layer independent of EF Core.
- Add/adjust tests for all changes.

## Commit & PR Process

- Use Conventional Commits: `feat:`, `fix:`, `docs:`, `refactor:`, `test:`, `chore:`
- Reference related issues in commit messages or PR description.
- Ensure:
  - Tests pass (`dotnet test`)
  - No analyzer errors
  - Migrations included when schema changes
  - Documentation updated (README/ADR where applicable)

## Reporting Issues

- Provide steps to reproduce, expected vs actual behavior, and logs/screenshots when possible.

## Code of Conduct

- Be respectful and collaborative. We value constructive feedback and inclusivity.

Thanks for making GoodFood better! 💜

