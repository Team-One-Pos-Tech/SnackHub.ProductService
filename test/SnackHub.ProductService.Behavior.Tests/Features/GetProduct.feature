Feature: GetProduct Use Case
    
    Scenario: Get product by id
        Given following products table
        | Id                                   | Name      | Category | Price | Description            |
        | 93eccf34-14e5-4c85-bb11-631d523e3ac6 | Rosquinha | 0        | 2.00  | Rosquinha de chocolate |
        | 919806ea-f94d-401b-a1f6-9a15291c4122 | X-Tudo    | 0        | 10.5  | Completo sem abacaxi   |
        | fea2f300-f37a-438f-9f10-f514ecd030b2 | Coca-cola | 2        | 9.45  | Coca gelada            |
        When search for the product with id '919806ea-f94d-401b-a1f6-9a15291c4122' it should return a Product Response with name 'X-Tudo'
        
    Scenario: Get products
        Given following products table
          | Id                                   | Name      | Category | Price | Description            |
          | 93eccf34-14e5-4c85-bb11-631d523e3ac6 | Rosquinha | 0        | 2.00  | Rosquinha de chocolate |
          | 919806ea-f94d-401b-a1f6-9a15291c4122 | X-Tudo    | 0        | 10.5  | Completo sem abacaxi   |
          | fea2f300-f37a-438f-9f10-f514ecd030b2 | Coca-cola | 2        | 9.45  | Coca gelada            |
        When list all products, it should return a List of Product with 3 elements
        
    Scenario: Get products by Category
        Given following products table
          | Id                                   | Name      | Category | Price | Description            |
          | 93eccf34-14e5-4c85-bb11-631d523e3ac6 | Rosquinha | 0        | 2.00  | Rosquinha de chocolate |
          | 919806ea-f94d-401b-a1f6-9a15291c4122 | X-Tudo    | 0        | 10.5  | Completo sem abacaxi   |
          | fea2f300-f37a-438f-9f10-f514ecd030b2 | Coca-cola | 2        | 9.45  | Coca gelada            |
        When list all products by category '0' , it should return a List of Product with '2' elements