using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreSqlDb.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    //[Column(TypeName = "nvarchar(100)")]
    [Column(TypeName = "VARCHAR(100)")]
    public required string FirstName { get; set; }

    [PersonalData]
    //[Column(TypeName = "nvarchar(100)")]
    [Column(TypeName = "VARCHAR(100)")]
    public required string LastName { get; set; }



}

