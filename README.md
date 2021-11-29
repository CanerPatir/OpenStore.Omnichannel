## OpenStore Omnichannel

- 

## Development

### Modules

- Storefront -> https://localhost:5005
- Panel -> https://localhost:5003
- Api -> https://localhost:5001
- Identity -> https://localhost:5001/identity/

## Marketplace Integration

- Attribute Mapping
- Category Mapping

## shopify demo
http://akeneo-shopify.webkul.com/demo.html

https://github.com/shopifypartners/product-csvs

## others
https://www.creative-tim.com/product/soft-ui-dashboard?utm_medium=social&utm_source=twitter&utm_campaign=soft+ui+dashboard

## mini todo
* remove Shared dependency from Domain
* update prevent exit if there is change
* variant count limit 

## store front todos
* Collection
* All 
* Product Detail
* Basket
* My Account
  * Orders
* Search
* StoreContext 

## see also 
- open telemetry, zipkin, jeager, promotheus, grafana 
- aot compalation 
- 


## Code Convention Dictionary

- Command -> Triggers behavior of aggregate root. Placed in domain layer
- Command Handler -> Encapsulates use case. Responsible for getting aggregate end dispatching command to related behavior. Also stores latest version of aggregate. Placed in application layer 
- Domain Events -> Represents result fact of a command. Should be immutable. "Record"s can be used to supply immutability 
- Query (Application Layer) -> Retrieves latest state of editing data. Probably supplies write model data to client to be editing. returns "dto"
- Query (Read Model) -> Retrieves read model. returns "result". results may contain dto. Read Model means eventual version 
- QueryHandler -> handler of any type of query. Either goes write model or read model. Can be used to supply inversion of dependency. 
- ViewModel (MVC - StoreFront) -> Minimum required data to render MVC view. Should be immutable
- ViewModel (MVVM - Panel) -> 