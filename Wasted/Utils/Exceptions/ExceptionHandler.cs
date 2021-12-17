using System;
using Xamarin.Forms;

namespace Wasted.Utils.Exceptions
{
    public static class ExceptionHandler
    {
        public static void WrapFunctionCall(Action function, ContentPage page)
        {
            try
            {
                function();
            }
            //TODO: ArgumentNullException doesn't mean that fields are empty, so perhaps new type of exception can be made
            //Maybe exception type and alert message can be passed as parameters, imo it will look better
            catch (ArgumentNullException)
            {
                page.DisplayAlert("", "Please fill all fields", "OK");
            }
            catch (UserAlreadyExistsException)
            {
                page.DisplayAlert("", "User with this username already exists", "OK");
            }
        }
    }
}