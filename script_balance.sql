use Balance

EXEC sp_rename 'Registration', 'Registry'

GO

ALTER TABLE PersonalAccountInvoice 
ALTER COLUMN PersonalAccountId int NOT NULL