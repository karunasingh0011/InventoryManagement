Create procedure [dbo].[AddNewInventoryItem]  
(  
   @Name varchar (200),  
   @Description varchar (500), 
   @Price decimal (10,2),
   @Image varchar (Max)  
)  
as  
begin  
   Insert into InvInfo values(@Name,@Description,@Price,@Image)  
End