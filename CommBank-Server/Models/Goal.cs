using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommBank.Models;

public class Goal
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Name { get; set; }

    public UInt64 TargetAmount { get; set; } = 0;

    public DateTime TargetDate { get; set; }

    public double Balance { get; set; } = 0.00;

    public DateTime Created { get; set; } = DateTime.Now;

    [BsonRepresentation(BsonType.ObjectId)]
    public string? AccountId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? TransactionIds { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? TagIds { get; set; }

    //Added Icon string
    public string? Icon { get; set; }

    //Added UserId: This field exits in the Git fork, but is missing in the later '1_backend.md' helper file, and is not mentioned in task.
    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }
}



/*
The biggest challenge I came across in this task was the missing UserId, not mentioned in the task. 

I believe there is a mistake in the code given in the '1_backend.md' helper file at:
https://github.com/fencer-so/commbank-program/blob/master/codelabs/1_backend.md

In short, we initially fork the repository and have a Goal.cs file with this as the last few lines:

[BsonRepresentation(BsonType.ObjectId)] 
public List<string>? TagIds { get; set; }
[BsonRepresentation(BsonType.ObjectId)] 
public string? UserId { get; set; }

--> Our task is to add an icon, and the .md helper file gives this suggestion:

[BsonRepresentation(BsonType.ObjectId)] 
public List<string>? TagIds { get; set; }

public string? Icon { get; set; }

 The suggested code removes the important UserID field, which causes a lot of bugs on the way
 Please update the '1_backend.md' instruction file if this was not intended to be a hidden 'challlenge' to debug. 
*/
