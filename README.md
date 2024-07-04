# VCard Generator

This is a simple C# console application that creates vCards using random user data from the RandomUser.me API. Each vCard includes information like name, email, phone, country, and city.

## Requirements

- .NET Core SDK

## How to Set Up

1. **Clone the repository:** Open cmd and write these commands

   ```bash
   git clone https://github.com/hasanhmbt/VCardCreator
   cd vcard-generator
   dotnet run
   ```

2. **Enter the number of vCards to create:**

- The app will ask you how many vCards you want to create. enter a number and press Enter.

3. **Check the generated vCards:**

- The vCards will be saved in the vCards folder in the project directory. Each file will be named like Firstname-Surname-UniqueID.vcf.

#### **Example Output:**

```bash
        Enter the number of VCards you want to create: 3
        vCard with ID: 123e4567-e89b-12d3-a456-426614174000 created.
        vCard with ID: 123e4567-e89b-12d3-a456-426614174001 created.
        vCard with ID: 123e4567-e89b-12d3-a456-426614174002 created.
        All vCards have been created.
```
#### **Note:**

- Network Errors: If there’s an issue connecting to the API, you’ll see an error message.
- File Errors: If there’s a problem saving a vCard, you’ll get an error message.
