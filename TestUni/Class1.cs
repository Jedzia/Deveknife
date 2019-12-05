//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Class1.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>04.02.2014 15:42</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace TestUni
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Schema;

    public class Class1234
    {
        private void Serialize_JSON()
        {
            /*Product product = new Product();
            product.Name = "Apple";
            product.Expiry = new DateTime(2008, 12, 28);
            product.Sizes = new string[] { "Small" };
            
            string json = JsonConvert.SerializeObject(product);
            */
            //{
            //  "Name": "Apple",
            //  "Expiry": "2008-12-28T00:00:00",
            //  "Sizes": [
            //    "Small"
            //  ]
            //}

        }
        private void Deserialize_JSON()
        {
            var json = @"{
  'Name': 'Bad Boys',
  'ReleaseDate': '1995-4-7T00:00:00',
  'Genres': [
    'Action',
    'Comedy'
  ]
}";

            //Movie m = JsonConvert.DeserializeObject<Movie>(json);

            //string name = m.Name;
            // Bad Boys
        }

        private void LINQ_to_JSON()
        {
            var array = new JArray();
            array.Add("Manual text");
            array.Add(new DateTime(2000, 5, 23));

            var o = new JObject();
            o["MyArray"] = array;

            var json = o.ToString();
            // {
            //   "MyArray": [
            //     "Manual text",
            //     "2000-05-23T00:00:00"
            //   ]
            // }
        }


        private void Validate_JSON()
        {
            JsonSchema schema = JsonSchema.Parse(@"{
  'type': 'object',
  'properties': {
    'name': {'type':'string'},
    'hobbies': {'type': 'array'}
  }
}");

            JObject person = JObject.Parse(@"{
  'name': 'James',
  'hobbies': ['.NET', 'LOLCATS']
}");

            bool valid = person.IsValid(schema);
            // true

        }
    }
}