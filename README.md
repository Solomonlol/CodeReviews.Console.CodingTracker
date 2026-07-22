#ABOUT CODING TRACKER

A .NET 10-based console application that allows users to create, view, update, and delete records of time spent coding.
It supports both manual data entry and automatic time tracking via a timer.
Users can view all database records in their original order or sort them in ascending or descending order.

#FEATURES

The program creates a database upon startup.
The user can select a menu item; navigation is performed using the arrow keys.
<img width="1734" height="927" alt="image" src="https://github.com/user-attachments/assets/11809f53-2559-4e24-867e-6b7262f44a2d" />

1. Print all records

This option give user another choises of how to print data
<img width="1734" height="927" alt="image" src="https://github.com/user-attachments/assets/5aba97f7-4b5a-46a3-830a-fb2a118851bf" />

Print - Output data without sorting
The other menu items speak for themselves.
After selecting the menu item, a table appears containing data read from the database.
<img width="1734" height="927" alt="image" src="https://github.com/user-attachments/assets/5c9c6fef-6e14-47dd-a14b-62d1a8bb7e1b" />

2.New record

This create new record. User write start and end time in 'dd.MM.yyyy H:m' format:
<img width="1734" height="927" alt="image" src="https://github.com/user-attachments/assets/ea318845-7a70-4c6a-bbd6-2baf4f7679fa" />

Data entered by the user must be correct. Otherwise, the program will indicate the incorrectly entered data.
3.Update record

This option allows the user to make changes to an existing record. 

4.Delete record

This option allows the user to delete an existing record.

5.Timer

This menu item starts a timer and displays the current session time on the console.
Once the user stops the timer, a new record containing the current session's timing data is created in the database.

6.Exit

This option close the app

#Lessons Learned

Learned how to use Stopwatch.
Tried creating a menu using a Dictionary instead of a switch statement.
Learned how to use Dapper—it is simpler than ADO.NET.
Gained experience in using a separate JSON file to store the database connection string.


#My thoughts

I still have a lot to improve, but progress is evident, as this project was completed faster 
than the previous one despite the use of new technologies.
