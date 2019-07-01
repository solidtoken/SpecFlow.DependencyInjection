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
- [ ] Move TODO to TODO+ document?
  - Too bad VS does not support TODO+ extension (it's VSCode only)
- [ ] Howto setup services
  - [ ] NuGet
  - [ ] Azure DevOps
  - [ ] GitHub

### CI/CD

- [x] Semantic versioning
  - GitVersion Build Task
- [x] GitHub workflow (?)
  - https://guides.github.com/introduction/flow/
  - Can't really get used to this, I prefer gitflow thus far
  - Trying to stick to it just to learn
- [x] CI -> Azure Pipelines
  - [x] Build using Azure Pipelines
  - [x] Smoke Test using the .Tests project
- [ ] CD -> NuGet
  - [x] Release a succesful build (gating?)
  - [ ] Signed packages
    - https://docs.microsoft.com/en-us/nuget/reference/signed-packages-reference
    - I'd like to sign the release commits (in git) as well (tags?)
    - Signed packages feels like a hassle with the Certification needed (and costs)
    - Probably best delayed until it's a really popular package
  - [ ] GitHub Releases? How and why?
    - [x] Implement GitHub Release from Azure DevOps
    - Still feels like you always need a manual step to release (vs GitFlow which allows you to auto-release)

### Code

- [x] Add .gitignore
- [ ] Add .editorconfig
- [ ] Add source code quality guidelines
- [x] Add SourceLink 
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

### Issues

- [ ] Determine minimal SpecFlow version to support
  - I now got an error that my project has a lower SpecFlow version than this plugin, which then fails
  - This is probably solved by a smart grep semver version thingy in package refs
- [ ] Documentation should include that you need to create a Setup Class/Method [ScenarioDependencies]