# SpecFlow.DependencyInjection

SpecFlow Plugin for Microsoft.Extensions.DependencyInjection

Based on https://github.com/gasparnagy/SpecFlow.Autofac.

## TODO

Todo will move to GitHub Issues once 1.0.0 is released. For now this README will have to suffice.
You can find me (@mbhoek) on https://gitter.im/techtalk/specflow-plugin-dev for questions and feedback.

### Documentation

- [ ] Proper README
- [x] Add LICENSE
- [ ] Make issue tracker available (1.0.0+)

### CI/CD

- [x] Semantic versioning
  - GitVersion Build Task
- [x] GitHub workflow (?)
  - https://guides.github.com/introduction/flow/
- [x] CI -> Azure Pipelines
  - [x] Build using Azure Pipelines
  - [x] Smoke Test using the .Tests project
- [ ] CD -> NuGet
  - [x] Release a succesful build (gating?)
  - [ ] Signed packages
    - https://docs.microsoft.com/en-us/nuget/reference/signed-packages-reference
    - I'd like to sign the release commits (in git) as well (tags?)

### Code

- [x] Add .gitignore
- [ ] Add .editorconfig
- [ ] Add source code quality guidelines
- [ ] Add SourceLink 
  - https://devblogs.microsoft.com/nuget/introducing-source-code-link-for-nuget-packages/
- [x] Set Assembly metadata correctly

### Tests

- [ ] Add tests for SpecFlow classes (ScenarioContext, FeatureContext, etc)
- [ ] Add tests for parallel testing
- [ ] Add tests for high-load testing
  - Test if we are properly disposing our plumbing code

### Social

- [ ] Publish builds/releases on social networks
  - [ ] Microsoft Teams
  - [ ] Slack
  - [ ] Twitter
  - [ ] LinkedIn
  - [ ] GitHub Releases
