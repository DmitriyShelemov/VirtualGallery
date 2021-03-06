-----------------------------------------------------------
-- Default Data
-----------------------------------------------------------
	DECLARE @orderInitial int = 0, 
			@maxOrder int = (SELECT MAX([Order]) FROM [dbo].[Categories]);

	DECLARE @nextOrder int = (SELECT MAX([value]) FROM (SELECT @maxOrder as [value] 
													UNION SELECT @orderInitial as [value]) AS T1);
	
	UPDATE [dbo].[Categories] 
	SET @nextOrder = @nextOrder + 1, [Order] = @nextOrder
	WHERE [Order] = 0