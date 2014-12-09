using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleSample.Model
{
 
    [TableName("UserInfo")]
    [PrimaryKey("Id",autoIncrement=true)]
    public class User
    {
        
        public long Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
