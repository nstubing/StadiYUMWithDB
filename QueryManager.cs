using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentDBTodo.Models;
using Microsoft.Azure.Documents.Spatial;

namespace DocumentDBTodo
{
    public partial class QueryManager
    {
        static QueryManager defaultInstance = new QueryManager();

        const string accountURL = @"https://stadiyumdb.documents.azure.com:443/";
        const string accountKey = @"NOIMnV5NeYZmDnT7KEjvSHnMpW11NKPshMJ5vwhtgGY5OvWmP98OzfWILP8sIBWR8EMlWcV7IInqxSlF0amcvQ==";
        const string databaseId = @"StadiYUMDB";
        const string userCollection = @"Users";
        const string orderCollection = @"Orders";
        const string concessionCollection = @"ConcessionStand";
        const string itemCollection = @"Food";
        const string stadiumCollection = @"Stadium";

        private Uri UserLink = UriFactory.CreateDocumentCollectionUri(databaseId, userCollection);
        private Uri OrderLink = UriFactory.CreateDocumentCollectionUri(databaseId, orderCollection);
        private Uri ConcessionLink = UriFactory.CreateDocumentCollectionUri(databaseId, concessionCollection);
        private Uri ItemLink = UriFactory.CreateDocumentCollectionUri(databaseId, itemCollection);
        private Uri StadiumLink = UriFactory.CreateDocumentCollectionUri(databaseId, stadiumCollection);

        private DocumentClient client;

        public QueryManager()
        {
            client = new DocumentClient(new System.Uri(accountURL), accountKey);
        }

        public static QueryManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }
        public async Task<bool> LoginEntry(User newUser)
        {
            var ExistingUser = client.CreateDocumentQuery<User>(UserLink, new FeedOptions { MaxItemCount = 1 }).Where(u=>u.Username==newUser.Username).AsEnumerable().FirstOrDefault();
            if (ExistingUser == null)
            {
                var thisUser = await client.CreateDocumentAsync(UserLink, newUser);
                var thisNewUser = client.CreateDocumentQuery<User>(UserLink, new FeedOptions { MaxItemCount = 1 }).Where(u => u.Username == newUser.Username).AsEnumerable().FirstOrDefault();
                App.currentUser = thisNewUser;
                return true;
            }
            else if (ExistingUser.Username == newUser.Username && ExistingUser.Password == newUser.Password)
            {
                App.currentUser= ExistingUser;
                return true;
            }
            else if (ExistingUser.Username == newUser.Username && ExistingUser.Password != newUser.Password)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
        public async Task SeatEnteredAsync(User thisUser)
        {
            var ExistingUser = client.CreateDocumentQuery<User>(UserLink, new FeedOptions { MaxItemCount = 1 }).Where(u => u.Username == App.currentUser.Username).AsEnumerable().FirstOrDefault();
            ExistingUser.CurrentSection = thisUser.CurrentSection;
            ExistingUser.Seat = thisUser.Seat;
            var update = await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, userCollection, ExistingUser.Id), ExistingUser);
            App.currentUser = ExistingUser;
            var currentseat = App.currentUser.Seat;
        }
        public async Task<List<Concession>> GetConcessions()
        {
            var theseConcessions = client.CreateDocumentQuery(ConcessionLink, new FeedOptions { MaxItemCount = -1 }).AsDocumentQuery();
            List<Concession> newList = new List<Concession>();
            while (theseConcessions.HasMoreResults)
            {
                newList.AddRange(await theseConcessions.ExecuteNextAsync<Concession>());
            }
            return newList;
        }
        public async Task AddItemToCart(Item item)
        {
            var openOrder = client.CreateDocumentQuery<Order>(OrderLink, new FeedOptions { MaxItemCount = 1 }).Where(o =>o.IsCartOrder==1).Where(o=>o.UserId==App.currentUser.Id).AsEnumerable().FirstOrDefault();
            if (openOrder!=null)
            {
                List<Item> thisOrder = new List<Item>();
                foreach(Item itemCart in openOrder.Items)
                {
                    thisOrder.Add(itemCart);
                }
                thisOrder.Add(item);
                openOrder.Items = thisOrder.ToArray();
                var update = await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, orderCollection, openOrder.Id), openOrder);
            }
            else
            {
                var LowerConcessions = client.CreateDocumentQuery<Concession>(ConcessionLink, new FeedOptions { MaxItemCount = 1 }).Where(o => o.Name==item.ConcessionName).Where(o=>o.Section<=App.currentUser.CurrentSection).AsEnumerable();
                var HigherConcessions = client.CreateDocumentQuery<Concession>(ConcessionLink, new FeedOptions { MaxItemCount = 1 }).Where(o => o.Name == item.ConcessionName).Where(o => o.Section > App.currentUser.CurrentSection).AsEnumerable();
                var Lower = LowerConcessions.OrderBy(l => l.Section).FirstOrDefault();
                var Higher = HigherConcessions.OrderByDescending(l => l.Section).FirstOrDefault(); 

                //var LowerSections = ThisConcesh.Where(c => c.Section <= App.currentUser.CurrentSection).OrderBy(b=>b.Section).FirstOrDefault();
                //var HigherSections = ThisConcesh.Where(c => c.Section > App.currentUser.CurrentSection).OrderByDescending(p=>p.Section).FirstOrDefault();
              
                Order myOrder = new Order();
                myOrder.UserId = App.currentUser.Id;
                myOrder.OrderedSection = App.currentUser.CurrentSection;
                myOrder.OrderedSeat = App.currentUser.Seat;
                myOrder.IsCartOrder = 1;
                myOrder.IsCompleted = 0;
                myOrder.Items = new Item[] { item };
                if(Higher==null)
                {
                    myOrder.ConcessionId = Lower.Id;
                }
                else if (LowerConcessions==null)
                {
                    myOrder.ConcessionId = Higher.Id;

                }
                else
                {
                    var LowNear = App.currentUser.CurrentSection - Lower.Section;
                    var HighNear = Higher.Section - App.currentUser.CurrentSection;
                    if (LowNear<HighNear)
                    {
                        myOrder.ConcessionId = Lower.Id;
                    }
                    else
                    {
                        myOrder.ConcessionId = Higher.Id;
                    }
                }
                var create = client.CreateDocumentAsync(OrderLink, myOrder);
                
            }

        }
        public void Stadiums()
        {
            var stadium = client.CreateDocumentQuery<Stadium>(StadiumLink, new FeedOptions { MaxItemCount = 1 }).Where(s => s.Id == "1").AsEnumerable().FirstOrDefault();
            App.CurrentStadium = stadium;
        }
        public List<Item> CartItems()
        {
            var openOrder =  client.CreateDocumentQuery<Order>(OrderLink, new FeedOptions { MaxItemCount = 1 }).Where(o => o.IsCartOrder == 1).Where(o => o.UserId == App.currentUser.Id).AsEnumerable().FirstOrDefault();
            List<Item> cartList = new List<Item>();
            if(openOrder !=null)
            {
                foreach(Item cartItem in openOrder.Items)
                {
                    cartList.Add(cartItem);
                }
            }
            return cartList;
        }
        public async void CartFinished()
        {
            var openOrder = client.CreateDocumentQuery<Order>(OrderLink, new FeedOptions { MaxItemCount = 1 }).Where(o => o.IsCartOrder == 1).Where(o => o.UserId == App.currentUser.Id).AsEnumerable().FirstOrDefault();
            openOrder.IsCartOrder = 0;
            var update = await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, orderCollection, openOrder.Id), openOrder);
            var Concessioner = client.CreateDocumentQuery<Concession>(ConcessionLink, new FeedOptions { MaxItemCount = -1 }).Where(c => c.Id == openOrder.ConcessionId).AsEnumerable().FirstOrDefault();
            List<Order> Orders = new List<Order>();
            foreach (Order Order in Concessioner.Orders)
            {
                Orders.Add(Order);
            }
            Orders.Add(openOrder);
            Concessioner.Orders = Orders.ToArray();
            var updateConcesh = await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, concessionCollection, Concessioner.Id), Concessioner);
        }
        public List<Order> GetOrders()
        {
            var orders = client.CreateDocumentQuery<Order>(OrderLink, new FeedOptions { MaxItemCount = 5 }).Where(o => o.UserId == App.currentUser.Id).Where(o => o.IsCartOrder == 0).AsEnumerable();
            return orders.ToList();
        }

        public Stadium GetClosestStadium(Point point)
        {
           
            var CloseStadium = client.CreateDocumentQuery<Stadium>(StadiumLink, new FeedOptions { MaxItemCount = 1 }).Where(S=>S.Location.Distance(point)<30).AsEnumerable().FirstOrDefault();
            return CloseStadium;
        }
        public List<Order> GetConcessionsOrders()
        {
            var Concession = App.currentUser.Username.ToCharArray();
            var letter = "A";
            var letterChar = letter.ToCharArray();
            var ConcessionQ = client.CreateDocumentQuery<Concession>(ConcessionLink, new FeedOptions { MaxItemCount = 1 }).Where(c => c.Name == "AJ Bombers").Where(c => c.Section == App.currentUser.CurrentSection).AsEnumerable().FirstOrDefault();
            List<Order> newList = new List<Order>();
            foreach(Order order in ConcessionQ.Orders)
            {
                if(order.IsCompleted==0)
                {
                    newList.Add(order);

                }
            }
            return newList;

        }
        public async void GetOrderToggle()
        {
            if (App.CurrentStadium.IsOpen==0)
            {
                App.CurrentStadium.IsOpen = 1;
            }
            else
            {
                App.CurrentStadium.IsOpen = 0;
            }
            var updated = await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, stadiumCollection, App.CurrentStadium.Id), App.CurrentStadium);
        }
        public async void CompleteOrder(Order CompletedOrder)
        {
            CompletedOrder.IsCompleted = 1;
            CompletedOrder.TimeCompleted = DateTime.Now;
            var update = await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, orderCollection, CompletedOrder.Id), CompletedOrder);
            var Concessioner = client.CreateDocumentQuery<Concession>(ConcessionLink, new FeedOptions { MaxItemCount = -1 }).Where(c => c.Id == CompletedOrder.ConcessionId).AsEnumerable().FirstOrDefault();
            List<Order> newList = new List<Order>();
            foreach(Order order in Concessioner.Orders)
            {
                if(order.Id==CompletedOrder.Id)
                {
                    order.IsCompleted = 1;
                    order.TimeCompleted = DateTime.Now;
                }
                newList.Add(order);
            }
            Concessioner.Orders = newList.ToArray();
            var updateConcesh = await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, concessionCollection, Concessioner.Id), Concessioner);
            
        }

        //public List<TodoItem> Items { get; private set; }

        //public async Task<List<TodoItem>> GetTodoItemsAsync()
        //{
        //    try
        //    {
        //        // The query excludes completed TodoItems
        //        var query = client.CreateDocumentQuery<TodoItem>(collectionLink, new FeedOptions { MaxItemCount = -1 })
        //              .Where(todoItem => todoItem.Complete == false)
        //              .AsDocumentQuery();

        //        Items = new List<TodoItem>();
        //        while (query.HasMoreResults)
        //        {
        //            Items.AddRange(await query.ExecuteNextAsync<TodoItem>());
        //        }


        //    }
        //    catch (Exception e)
        //    {
        //        Console.Error.WriteLine(@"ERROR {0}", e.Message);
        //        return null;
        //    }

        //    return Items;
        //}

        //public async Task<TodoItem> InsertItemAsync(TodoItem todoItem)
        //{
        //    try
        //    {
        //        var result = await client.CreateDocumentAsync(collectionLink, todoItem);
        //        todoItem.Id = result.Resource.Id;
        //        Items.Add(todoItem);


        //    }
        //    catch (Exception e)
        //    {
        //        Console.Error.WriteLine(@"ERROR {0}", e.Message);
        //    }
        //    return todoItem;
        //}

        //public async Task CompleteItemAsync(TodoItem item)
        //{
        //    try
        //    {
        //        item.Complete = true;
        //        await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, item.Id), item);

        //        Items.Remove(item);

        //    }
        //    catch (Exception e)
        //    {
        //        Console.Error.WriteLine(@"ERROR {0}", e.Message);
        //    }
        //}
    }
}

