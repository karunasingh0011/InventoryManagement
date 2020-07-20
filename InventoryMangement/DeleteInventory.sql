Create procedure [dbo].[DeleteInventory]  
(  
   @InvId int  
)  
as   
begin  
   Delete from InvInfo where Id=@InvId  
End