select
    top :PageSize
    Customer.Id as CustomerId,
    Customer.Alias as CustomerAlias,
    CustomerRoot.Name as CustomerRootName
from
    monitor.Customer
    inner join monitor.CustomerRoot on CustomerRoot.Id = Customer.RootId
where
    (Customer.Alias like :Param1 or CustomerRoot.Name like :Param1)
    and (Customer.Alias like :Param2 or CustomerRoot.Name like :Param2)
    and (Customer.Alias like :Param3 or CustomerRoot.Name like :Param3)
    and (Customer.Alias like :Param4 or CustomerRoot.Name like :Param4)
    and (Customer.Alias like :Param5 or CustomerRoot.Name like :Param5)
order by
    Customer.Alias