# RMVMT Assessment

Assessment using ASP.NET Core Web App with Razor Pages and Entity Framework + SQLite for storage.
___
The assessment is a application to handle a simple Driver Licence Suspension operations based on the following business rules: 
* Determining whether the individual holds a valid driver licence, or not.
* Determining whether the driver licence status is active (i.e. not expired), then adding a suspension to the driver record.
    * A valid/active driver licence status would be set to suspended.
    * A suspended driver licence status would remain suspended; i.e. there are other suspension transactions already on the licence .
* Delete/archive any suspension transactions that have expired, e.g. are outside 5-year timeframe.
* Mark a suspension transaction as expired if the end date is in the past.
* Persist the suspensions in a data store.
___
## Application setup

### Running the application

To run the app:
1. Clone the app repository with your favorite GIT client.
1. Open the solution.
1. Run the app.