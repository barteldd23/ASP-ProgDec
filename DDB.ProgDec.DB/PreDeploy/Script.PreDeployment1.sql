﻿/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


DROP TABLE IF exists tblDeclaration
DROP TABLE IF EXISTS tblDegreeType
DROP TABLE IF EXISTS tblProgram
DROP TABLE IF EXISTS tblStudent
DROP TABLE IF EXISTS tblUser
DROP TABLE IF EXISTS tblAdvisor
DROP TABLE IF EXISTS tblStudentAdvisor