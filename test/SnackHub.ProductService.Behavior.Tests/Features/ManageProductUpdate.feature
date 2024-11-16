Feature: ManagedProduct Use Case - Update a Product
    
    Scenario: Update a product
        Given a product with name 'Rosquinha', category '0', price '2.00', and description 'Rosquinha de chocolate'
        Then a product with name 'Rosquinha' should exist
        Then an event of type 'ProductCreated' is raised
        When edit the product named 'Rosquinha' to have the name 'Donuts'
        Then an event of type 'ProductUpdated' is raised
        Then a product with name 'Donuts' should exist
        