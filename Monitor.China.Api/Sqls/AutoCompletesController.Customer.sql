select
    top 15
    Customer.Id as CustomerId,
    Customer.Alias as CustomerAlias,
    CustomerRoot.Name as CustomerRootName
from
    monitor.Customer
    inner join monitor.CustomerRoot on CustomerRoot.Id = Customer.RootId
where
    (Customer.Alias like '%1%' or CustomerRoot.Name like '%1%')
    and (Customer.Alias like '%2%' or CustomerRoot.Name like '%2%')
order by
    Customer.Alias