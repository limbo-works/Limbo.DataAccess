# Changelog

All notable changes to this project will be documented in this file. See [standard-version](https://github.com/conventional-changelog/standard-version) for commit guidelines.

## [2.0.0-0](https://github.com/limbo-works/Limbo.DataAccess/compare/v1.1.1...v2.0.0-0) (2022-04-28)


### ⚠ BREAKING CHANGES

* Changed extension call signature
* Changed constructor of DbCrudRepositoryBase

Changed call signature on RemoveFromCollection and AddToCollection

fix: Transation not null in beginUnitOfWork

fix: Transation not disposed

refactor: Refactored ServiceBase dependency on UnitOfWork

fix: Fixed DbContext being disposed mid method by using DbContextFactory allowing scoped DbContexts

fix: Added inner exceptions to Task Failed errors

fix: Fixed collections not loaded on AddToCollection and RemoveFromCollection leading to strange behaviour

chore(deps): Updated dependencies

fix: Fixed spelling error of DataAccessExtensions class

test: Added boilerplate for tests

### Features

* Changed extension parameters to classes ([8644432](https://github.com/limbo-works/Limbo.DataAccess/commit/864443229232726310f61d799152a73adbe33c5e))


### Bug Fixes

* Fixed dbcontext disposed in some cases ([b4bda20](https://github.com/limbo-works/Limbo.DataAccess/commit/b4bda2059d849563170d9f86c9943332371c6007))
* Fixed multiple bugs (See description) ([ca80184](https://github.com/limbo-works/Limbo.DataAccess/commit/ca801842621e0951c82ba4b835e4c17b9b48b159))
* Fixed relation data would be reset ([ff79940](https://github.com/limbo-works/Limbo.DataAccess/commit/ff79940b0de58733915dcf6313827ebad217270d))
* Fixed spelling error ([15e6dd2](https://github.com/limbo-works/Limbo.DataAccess/commit/15e6dd2b85733498fd1ebc0a5c99641f3a9bb245))
* Unit of work set wrong dbcontext ([912f256](https://github.com/limbo-works/Limbo.DataAccess/commit/912f25644d894fbaae9790150f3a9bfaf5ec77e7))

### [1.1.1](https://github.com/limbo-works/Limbo.DataAccess/compare/v1.1.0...v1.1.1) (2022-03-03)


### Bug Fixes

* Fixed methods not marked virtual ([51ca611](https://github.com/limbo-works/Limbo.DataAccess/commit/51ca611f1e337fd28025378eff3fdd8d20d62a0d))

## [1.1.0](https://github.com/limbo-works/Limbo.DataAccess/compare/v1.0.0...v1.1.0) (2022-03-03)


### Features

* Added changes from sister repo ([e80a7fb](https://github.com/limbo-works/Limbo.DataAccess/commit/e80a7fbe9caad72a15d4348f2a7c9b0390b2effd))

## 1.0.0 (2022-02-21)
