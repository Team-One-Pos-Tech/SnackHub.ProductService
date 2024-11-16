Feature: ManagedProduct Use Case - Create Product
    
    Scenario: Create a non duplicated product
        Given a product with name 'Rosquinha', category '0', price '2.00', and description 'Rosquinha de chocolate'
        Then a product with name 'Rosquinha' should exist
        Then an event of type 'ProductCreated' is raised