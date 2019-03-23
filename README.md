Timesheet Generator
===================

Create timesheet Excel with a tab per month with Belgian holidays.

**Send to**:  
- Freelancer: invoice@itenium.be
- Consultant: timesheet@itenium.be


New Project
-----------

A new project where we use our timesheet template.
Add a json file to `Itenium.Timesheet.Console\Projects`.

Example  
```
{
  "ConsultantName": "",
  "Customer": "",
  "CustomerReference": "",
  "ProjectName": "",
  "IsFreelancer": 0
}
```


New Year
--------

Create the templates:  
```
dotnet run --project .\Itenium.Timesheet.Console\Itenium.Timesheet.Console.csproj
```

Output:  
- Generic template for Consultants
- Generic template for Freelancers
- Specific project template per json file in `Projects` folder

And then save them on the OneDrive in the folder `internal process`.

Or use the Desktop application (Itenium.Timesheet.WinForms) to fill in the parameters with a UI.
