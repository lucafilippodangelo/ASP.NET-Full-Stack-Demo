using GigHub.Controllers;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate] //LDP1_002
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                /* //LD some theory:
                 * TO RESUME, this lampda "c => c.Update(this)" represent a method that take "c" as parameter 
                 * and return an "ActionResult". "this" is the view model.
                 * I can use "Func<xxx,xxx>" that is a DELEGATE to represent that. So "Func" is the delegate that 
                 * allow us to call the anonymus method. 
                 * Depend on the parameter I will I will get the anme of the method at runtime.
                 
                 */

                //LDP2_002
                //LD old way to do
                //return (Id != 0) ? "Update" : "Create";

                //this expression rapresent the "Update" action in the GigController.
                Expression<Func<GigsController, ActionResult>> update =
                    (c => c.Update(this));

                Expression<Func<GigsController, ActionResult>> create =
                    (c => c.Create(this)); 

                var action = (Id != 0) ? update : create;

                //we get the action name at runtime
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }

    }
}