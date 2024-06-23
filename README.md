# Kiron

I created all projects using .Net 7

The project is structured with reusability in mind and solid principles

What does the project have – overview

    o   Global Exception handling – handled by middleware

    o   Fluent Validation – register , search by region

    o   OneOf – Region Holidays

    o   AutoMapper and everything requested
    

How to run the project

1.       SQLBackups Folder =>  KironTest.bak ( this the file I was sent)

2.       Please use KironQA for the db name or you may change it in the appsetting.json(update the connection string) 
3.       Once that completed you should have just the navigation table with its data

4.       Run the KironBackUp.sql file to this db

5.       The db should have 6 stored procs and 4 additional tables , with only the region and user table populated with some default values

6.       Once you run the application the background service will trigger and populate the db with the holidays and linked regions 

7.       Then will retrigger every 24 hours, can be changed in the appsettings HostedServicesDurationHourDelay


You can use this admin@gmail.com  1234 for getting a token or use the register to get your details ( rem to use **bearer** yourtoken for auth) 
