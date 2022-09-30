/**
 * :::::::::::::::::::NOTICE:::::::::::::::::::::::::
 * This is a derived class of class IdentityUser
 * AppUser inherits  the fields and methods of an IdentityUser
 * which is created within ASP.NET when selecting
 * 'individual user accounts' in project creation.
 * 
 * An IdentityUser is created when a user registers an account.
 * ASP.NET conventions recommend not directly editing an 
 * IdentityUser.
 * 
 * When needed, access user data via an AppUser object. 
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tavern.Data.Migrations;
using System.Diagnostics;

namespace Tavern.Models
{
    public class AppUser : IdentityUser
    {
        //Try adding an AppUser Table with a new ID field that will be used to link AppUsers to campaigns and characters
        //Instead of trying to link it to the userID's from IdentityUser
        public ICollection<Campaign>? Campaigns { get; set; }

        public ICollection<Character>? Characters { get; set; }

        public void testId()
        {
            Debug.WriteLine("ID OF CURRENT USER: " + Id);
        }
    }
}
