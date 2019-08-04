# winforms-not-paid
This class library adds a extension method to a ``Windows.Forms.Form`` which lowers the ``Form.Opacity`` based on how close 
``DateTime.Today`` is from a given ``DateTime`` due date.

If ``DateTime.Today`` is past due date, the ``Form.Opacity`` is set to 0 and the ``Form.Controls`` is cleared.

## Usage
1. Add the class library to your project References.
2. Import the class library in your Form ``.cs``
```csharp
using WinformsNotPaid;
```
3. Inside the Form constructor, after ``InitializeComponent()``, call the ``ChangeNotPaidOpacity`` method:
```csharp
public Form1()
{
    InitializeComponent();
    this.ChangeNotPaidOpacity(DateTime.Parse("Aug 25, 2019"), 30);
}
```
### ChangeNotPaidOpacity method
*(couldn't think of a better name)*
It takes two parameters:
- dueDate: A ``System.DateTime`` that marks the last day before the Opacity is set to 0 and all the controls are removed.
- daysDeadline: A integer that says how many days before the dueDate should the Opacity start to lower.

## Author
This was inspired and based of [klempa](https://github.com/kleampa)'s [not-paid](https://github.com/kleampa/not-paid).
