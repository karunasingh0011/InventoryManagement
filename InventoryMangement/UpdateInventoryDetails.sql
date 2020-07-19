Create procedure [dbo].[UpdateInventoryDetails]  
(  
   @InvId int,  
   @Name varchar (200),  
   @Description varchar (500), 
   @Price decimal (10,2),
   @Image varchar (Max) 
)  
as  
begin  
   Update InvInfo   
   set Name=@Name,  
   Description=@Description,  
   Price=@Price  ,
   Photo=@Image
   where Id=@InvId  
End