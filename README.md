# Hatfield.EnviroData.WQDataProfile

The Water Quality data profile sits on top of the ODM2 database and provides a standardized way of dealing with sampled water quality data. All systems (data aquisition, quality assurance and reporting) should interface with this data profile, and not directly with ODM2. The data profile is automatically generated after the data import process. The generated profile contains entities and their relations, which are defined both from the data source and ODM2 data. All data analysis is done through this water quality data profile, which allows users to interact with the generated entities, without needing any knowledge of the schema. 

![ERD Relationship](/docs/images/ERD_1.png)

A brief description of each entity is provided below:

**Projects:**
Contains project details such as project name.

**Facilities:**
Copies some  of the information from the Projects entity. Has a many-to-many relationship with the Projects entity.

**Monitoring Locations:**
Contains sampling site information; geographic location details, site name and site id.

**Secondary Site IDs:**
Secondary sites that are a subset of the primary Monitoring Locations entity. 

**Samples:**
Contains information about the sampling feature. Entity contains information such as Depth, Sample Code, and Sample Type.

**Sub-samples:**
Is a sub-group of a primary sample.

**Organization:**
Entity representing group of people or a company responsible for conducting the project. Has a many-to-many relationship with the Projects entity.

**Field Crews:** 
Contains contact details for Field Crew members responsible for testing of samples

**Laboratory:**
Represents the laboratory responsible for conducting tests on the extracted samples.

**Field Methods:**
Entity representing name and type of field methods used to conduct field tests on extracted samples.

**Laboratory Methods:**
Entity representing name and type of laboratory method used to conduct laboratory tests on extracted samples.

**Measurement Results:**
Entity representing the final output of each profile. The result has information such as Sample Code, Analysed Date, Result Value, Result Value's Unit and Result Type.

