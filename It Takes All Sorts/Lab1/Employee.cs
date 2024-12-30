using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// This class describes the employee
/// </summary>
public class Employee
{
  private string name;                          // The employee name
  private int number;                           // The employee ID
  private decimal rate;                         // The hourly rate
  private double hours;                         // The weekly hours
  private decimal gross;                        // The gross pay

  /// <summary>
  /// Default constructor for Employee - used for creating Employee array
  /// </summary>
  public Employee()
  {
  }

  /// <summary>
  /// Four-argument constructor for Employee
  /// </summary>
  /// <param name="name">Employee name</param>
  /// <param name="number">Employee number</param>
  /// <param name="rate">Hourly rate of pay</param>
  /// <param name="hours">Hours worked in a week</param>
  public Employee(string name, int number, decimal rate, double hours)
  {
        /* This is the better way to set data in a class - use the accessor methods.
         * That way, if their implementation changes, the constructor doesn't need to
         * be edited as well.
         */
        Name = name;
        Number = number;
        Rate = rate;
        Hours = hours;

        gross = Gross;
  }

  /// <summary>
  /// Properties Gross get
  /// </summary>
  /// <returns>Gross pay for the week</returns>
  public decimal Gross {
        get { return (hours < 40) ? (decimal)hours * rate : (40.0m * rate) + (((decimal)hours - 40.0m) * rate * 1.5m); }
    }

  /// <summary>
  /// Properties Hours get and set
  /// </summary>
  /// <returns>Hours worked this week</returns>
  public double Hours {
        get { return hours; }
        set { hours = value; }
    }

  /// <summary>
  /// Properties Name get and set
  /// </summary>
  /// <returns>Employee name</returns>
  public string Name {
        get { return name; }
        set { name = value; }
    }

  /// <summary>
  /// Properties Number get and set
  /// </summary>
  /// <returns>Employee number</returns>
  public int Number {
        get { return number; }
        set { number = value; }
    }

  /// <summary>
  /// Properties Rate get and set
  /// </summary>
  /// <returns>Hourly rate of pay</returns>
  public decimal Rate {
        get { return rate; }
        set { rate = value; }
    }

  /// <summary>
  /// Employee display method - in the future, we'll override the ToString method of Object
  /// </summary>
  public override String ToString() => $"{name,-20}  {number:D5}  {rate,6:C}  {hours:#0.00}  {gross,9:C}";
}

