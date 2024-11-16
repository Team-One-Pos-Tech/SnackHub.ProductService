Feature: ManagedProduct Use Case - Delete a Product
    
    Scenario: Delete a product
        Given a product with name 'Rosquinha', category '0', price '2.00', and description 'Rosquinha de chocolate'
        Then a product with name 'Rosquinha' should exist
        Then an event of type 'ProductCreated' is raised
        When delete the product with name 'Rosquinha'
        Then an event of type 'ProductDeleted' is raised
        Then the product named 'Rosquinha' should not exists anymore