using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wasted.Utils;
using Wasted.Utils.Services;
using Wasted.WastedAPI.Business_Objects.Users;

namespace Wasted.WastedAPI
{
    public static class UsersProvider
    {
        private const string UsersRoute = "users";
        public const string ClientUsersRoute = "clientusers";
        public const string PlaceUsersRoute = "placeusers";

        private static readonly Dictionary<string, Func<string, User>> UserGettingFunctions = new Dictionary<string, Func<string, User>>
        {
            {Constants.ClientType, JsonConvert.DeserializeObject<ClientUser>},
            {Constants.PlaceType, JsonConvert.DeserializeObject<PlaceUser>}
        };
        
        public static ClientUser GetClientUser(UserDetailsService service)
        {
            return  DataFetchingUtils.GetBusinessObject<ClientUser>(ClientUsersRoute, service.UserName,
                service.UserPassword);
        }

        public static void CreateClientUser(ClientUser clientUser)
        {
            DataFetchingUtils.CreateBusinessObject(clientUser, ClientUsersRoute);
        }

        public static PlaceUser GetPlaceUser(UserDetailsService service)
        {
            return DataFetchingUtils.GetBusinessObject<PlaceUser>(PlaceUsersRoute, service.UserName,
                service.UserPassword);
        }

        public static void CreatePlaceUser(PlaceUser placeUser)
        {
            DataFetchingUtils.CreateBusinessObject(placeUser, PlaceUsersRoute);
        }

        public static User GetUser(string username, string password)
        {
            string json = DataFetchingUtils.GetJson(UsersRoute, username, password);
            string userType = JObject.Parse(json)[Constants.UserType]?.ToString();
            if (userType == null)
                throw new JsonException($"Error. Expected {Constants.UserType} attribute in user json.");

            return UserGettingFunctions[userType].Invoke(json);
        }
    }
}