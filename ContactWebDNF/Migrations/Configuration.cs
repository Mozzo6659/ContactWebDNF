namespace ContactWebDNF.Migrations
{
    using ContactWebDNF.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            CreateStates(context);
                
        }

        private void CreateStates(ApplicationDbContext context)
        {
            //Find if state in List already exists in the database
            foreach (var state in SeedStates)
            {
                var exist = context.States.FirstOrDefault(x => x.Abbreviation.Equals(state.Abbreviation, StringComparison.OrdinalIgnoreCase));

                if (exist == null) {
                    context.States.Add(state);
                }

            }

            context.SaveChanges();

        }

        private static List<State> SeedStates = new List<State> {

            new State() {Abbreviation="NSW",Name="New South Wales"},
            new State() {Abbreviation="VIC",Name="Victoria"},
            new State() {Abbreviation="QLD",Name="Queensland"}

        };


    }
}
