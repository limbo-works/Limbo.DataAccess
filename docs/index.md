# Documentation

## Contexts
The context model IDbContext creates a base for referencing a DbContext this will need to be added to the DbContext created in your project.

[See how to setup your DbContext](contexts/howToSetupDbContext.md)

## Conventions
The DefaultValues class includes some default values that can be used to limit string lengths 

## Models

### IGenericId
This class is used to interact with the id on models and therefore requires a Id property on a model. This is required to be used for using the generic repositories.

[See how to setup a model with IGenericId](Models/IGenericId/howToSetupModel.md)

## Repositories

### Bases
The bases contain very litte funtionallity and should be used for creating new generic models.

### CRUD
The CRUD bases are used for adding CRUD funtionallity to a entity. Every method is virtual and can be overridden.

[See how to create a CRUD repository](Repositories/CRUD/howToCreateACRUDRepository.md)

## Services

### Bases
The bases contain very litte funtionallity and should be used for creating new generic models.

### CRUD
The CRUD bases are used for adding CRUD funtionallity to a entity. Every method is virtual and can be overridden.

[See how to create a CRUD service](Services/CRUD/howToCreateACRUDService.md)

## Unit of work
The unit of work classes provide funtionallity to excecute queries with transactions.