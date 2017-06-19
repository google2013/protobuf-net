﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using ProtoBuf;

namespace Examples.Issues
{
    
    public class Issue310
    {
        [Fact]
        public void Execute()
        {
#pragma warning disable  0618
            string proto = Serializer.GetProto<Animal>();

            Assert.Equal(@"syntax = ""proto2"";

package Examples.Issues;

message Animal {
   optional int32 NumberOfLegs = 1 [default = 0];
   // the following represent sub-types; at most 1 should have a value
   optional Cat Cat = 2;
   optional Dog Dog = 3;
}
message Cat {
   repeated Animal AnimalsHunted = 1;
}
message Dog {
   optional string OwnerName = 1;
}
", proto);
        }

        [ProtoContract]
        [ProtoInclude(2, typeof(Cat))]
        [ProtoInclude(3, typeof(Dog))]
        public class Animal
        {
            [ProtoMember(1)]
            public int NumberOfLegs { get; set; }
        }

        [ProtoContract]
        public class Dog : Animal {
            [ProtoMember(1)]
            public string OwnerName { get; set; }
        }
        
        [ProtoContract]
        public class Cat : Animal
        {
            [ProtoMember(1)]
            public List<Animal> AnimalsHunted { get; set; }
        }
    }
}
