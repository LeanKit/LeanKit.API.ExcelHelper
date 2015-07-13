## LeanKit.API.ExcelHelper

LeanKit API Client for Excel and other Microsoft Office VBA or COM-based applications.

### Requirements

* .NET Framework 4.0
* [Visual Studio Community 2013](https://www.visualstudio.com/en-us/products/visual-studio-community-vs.aspx) or higher
* Windows 7 or higher, 64-bit
* Microsoft Excel 2010 or higher

### Deploying the Utility

1. Clone this repository
1. Compile using Visual Studio
1. Deploy and [register](https://msdn.microsoft.com/en-us/library/tzat5yw6(v=vs.100).aspx) `LeanKit.API.ExcelHelper.dll` 

	* Note: Compiling the software will automatically register the assembly on the current computer. The following steps are only required to register the component on other computers.
	* Copy all the files in `/bin/Debug/` to the destination computer
	* Open a command prompt as an Administrator 
	* Change to the directory where LeanKit.API.ExcelHelper.dll is located
	* Run `C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe .\LeanKit.API.ExcelHelper.dll`

1. Open Excel
1. Open the Visual Basic editor (ALT+F11)
1. Go to Tools -> References...
1. Find and check "LeanKit API Client for Excel" and click OK

### Example macro code for Excel VBA:

```
Public Sub TestCreateLeanKitCard()
    Dim lkClient As New LeanKit_API_ExcelHelper.Client
    Dim accountName As String
    Dim accountEmail As String
    Dim accountPassword As String
    
    Dim boardId As LongLong
    Dim cardId As LongLong
    Dim laneId As LongLong
    Dim title As String
    Dim description As String
    Dim cardType As String
    Dim customIcon As String
    Dim priority As Long
    Dim parentCardId As LongLong
    Dim cardSize As Long
    Dim cardIndex As Long
    Dim isBlocked As Boolean
    Dim blockedReason As String
    Dim externalCardId As String
    Dim externalSystemName As String
    Dim externalSystemUrl As String
    Dim startDate As String
    Dim dueDate As String
    Dim tags As String
    Dim assignedUsers As String
    Dim commentText As String
    
    accountName = "your-account-name"
    accountEmail = "your@email-address.com"
    accountPassword = "your-p@ssw0rd"
    
    boardId = 63454169
    laneId = 0 ' Enter the lane id, or leave as 0 to create card in default drop lane
    title = "Excel Helper Test Card (from Excel)"
    description = "Description of card"
    cardType = "Task"
    customIcon = "Bug" ' Optional Custom Icon (formerly named "class of service"), leave empty for no custom icon
    priority = 1
    parentCardId = 0
    cardSize = 0
    cardIndex = 0
    externalCardId = "123"
    externalSystemName = "Excel Helper"
    externalSystemUrl = ""
    isBlocked = True
    blockedReason = "Because reasons"
    startDate = "2015/07/25" ' Date format should match the account date format settings
    dueDate = "2015/08/25" ' Date format should match the account date format settings
    tags = "tag1, tag2"
    assignedUsers = "assigned@email-address.com" ' Comma-separated list of email addresses
    commentText = "Testing a comment"
    
    lkClient.Initialize accountName, accountEmail, accountPassword
    
    cardId = lkClient.AddCard(boardId, title, description, priority, cardSize, cardIndex, _
        laneId, cardType, customIcon, parentCardId, isBlocked, blockedReason, _
        externalCardId, externalSystemName, externalSystemUrl, _
        startDate, dueDate, tags, assignedUsers)
    
    lkClient.AddCardComment boardId, cardId, commentText
    
    MsgBox "Created card: " & cardId
 
End Sub
```