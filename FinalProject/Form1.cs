//Author: Ali AL-Habarneh
//Date: 03/12/2023
//Purpose: Book Tracker that takes books user enters and keeps them stored into a database.
using System.ComponentModel;
using System.Data.SqlClient;


namespace FinalProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        //Global Book Object
        private Book selectedBookObject;

        //Create a bindinglist for book class
        private BindingList<Book> bookList = new BindingList<Book>();

        int bookLastNumber = 0;

        private void enterButton_Click(object sender, EventArgs e)
        {

            //Declare Book Object
            var bookObject = new Book();

            //Declare Variables
            int intChecker;

            //Validate user inputs
            if ((this.titleTextBox.Text ?? "") == (string.Empty))
            {
                MessageBox.Show("You must enter a Title");
                titleTextBox.Focus();
                return;
            }
            else if ((this.authorTextBox.Text ?? "") == (string.Empty))
            {
                MessageBox.Show("You must enter an Author");
                authorTextBox.Focus();
                return;
            }
            else if ((dateTextBox.Text ?? "") == (string.Empty))
            {
                MessageBox.Show("You must enter a Date");
                dateTextBox.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(ratingComboBox.Text))
            {
                MessageBox.Show("Rating must be numeric");
                ratingComboBox.Focus();
                return;
            }
            else if (int.TryParse(goalTextBox.Text, out intChecker) == false)
            {
                MessageBox.Show("Goal must be numeric");
                goalTextBox.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(genreComboBox.Text))
            {
                MessageBox.Show("You must select a genre from the dropdown menu");
                genreComboBox.Focus();
                return;
            }
            else if ((descriptionTextBox.Text) == (string.Empty))
            {
                MessageBox.Show("You must enter a description about your book");
                descriptionTextBox.Focus();
                return;
            }
            else
            {

                if (bookIDTextBox.Text == String.Empty)
                {
                    bookLastNumber++;
                    bookObject.BookID = bookLastNumber;
                }
                else
                {
                    bookObject.BookID = int.Parse(bookIDTextBox.Text);
                }
                //Assign object properties to textboxes
                bookObject.Title = titleTextBox.Text;
                bookObject.Author = authorTextBox.Text;
                bookObject.Rating = int.Parse(ratingComboBox.Text);
                bookObject.Goal = int.Parse(goalTextBox.Text);
                bookObject.Genre = genreComboBox.SelectedItem.ToString();
                bookObject.Description = descriptionTextBox.Text;
            }

            //The new book is the seleceted book
            selectedBookObject = bookObject;
            //Add new Book to the list of Books
            bookList.Add(bookObject);
            bookListBox.SelectedItem = bookObject;
            //Display the results
            DisplayBookInfo();
            //Insert the book into database
            InsertBook();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //Populate genre comboBox on form load
            genreComboBox.Items.Add("Fiction");
            genreComboBox.Items.Add("Science Fiction");
            genreComboBox.Items.Add("Historical Fiction");
            genreComboBox.Items.Add("Non-Fiction");
            genreComboBox.Items.Add("Fantasy");
            genreComboBox.Items.Add("Biography");
            genreComboBox.Items.Add("Fiction");
            genreComboBox.Items.Add("Horror");
            genreComboBox.Items.Add("Mystery");
            genreComboBox.Items.Add("Thriller");
            genreComboBox.Items.Add("Romance");

            //Make the first option the selected option on form load
            genreComboBox.SelectedIndex = 0;

            //Populate ratings on form load
            ratingComboBox.Items.Add("1");
            ratingComboBox.Items.Add("2");
            ratingComboBox.Items.Add("3");
            ratingComboBox.Items.Add("4");
            ratingComboBox.Items.Add("5");

            //Make the first option the selected option on form load
            ratingComboBox.SelectedIndex = 0;

            //Bind the bookList BindingList to the bookListBox
            bookListBox.DataSource = bookList;
            bookListBox.DisplayMember = "Title";

            //Load Books
            ReloadBooks();
        }



        private void clearButton_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = string.Empty;
            authorTextBox.Text = string.Empty;
            dateTextBox.Text = string.Empty;
            ratingComboBox.SelectedIndex = 0;
            goalTextBox.Text = string.Empty;
            genreComboBox.SelectedIndex = 0;
            descriptionTextBox.Text = string.Empty;

            ClearLabels();
        }

        private void DisplayBookInfo()
        {
            string strBooks;
            if (selectedBookObject.Goal <= 1)
            {
                strBooks = " Book";
            }
            else
            {
                strBooks = " Books";
            }

            string strRating = " out of 5";

            titleOutputLabel.Text = selectedBookObject.Title;
            authorOutputLabel.Text = selectedBookObject.Author;
            dateOutputLabel.Text = selectedBookObject.Date.ToString();
            ratingOutputLabel.Text = selectedBookObject.Rating.ToString() + strRating;
            goalOutputLabel.Text = selectedBookObject.Goal.ToString() + strBooks;
            genreOutputLabel.Text = selectedBookObject.Genre.ToString();
            descOutputLabel.Text = selectedBookObject.Description.ToString();
        }

        private void bookListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeSelectedBook();
        }

        private void ChangeSelectedBook()
        {
            if (bookListBox.SelectedItem is object)
            {
                selectedBookObject = (Book)bookListBox.SelectedItem;
                DisplayBookInfo();
            }
            else
            {
                selectedBookObject = null;
                DisplayBookInfo();
            }
        }

        private SqlConnection OpenConnection()
        {

            //Get full path into bin/debug folder
            string path = Application.StartupPath;
            int pathLength = path.Length;

            //Strip off bin/debug folder to get into project folder
            path = path.Substring(0, pathLength - 25);

            //Create connection string
            string connection = @"Server=(LocalDB)\MSSQLLOcalDB;Integrated Security=true;AttachDbFileName=" + path + "BookDatabase.mdf";

            //Create a connection object to book database
            var bookConnection = new System.Data.SqlClient.SqlConnection(connection);

            //Open the connection to the database
            bookConnection.Open();

            return bookConnection;
        }

        private void InsertBook()
        {
            //Open the database
            var connection = OpenConnection();

            //Create SQL Insert string
            string insertSQL = "Insert into Book_Tbl(Title,Author,Date,Rating,Goal,Genre,Description) values (@Title, @Author, @Date, @Rating, @Goal, @Genre, @Description)";
            MessageBox.Show(insertSQL.ToString());

            //Create Command
            var insertCommand = new SqlCommand(insertSQL, connection);

            //Populate the parameters of the insert
            insertCommand.Parameters.AddWithValue("Title", bookList.Last().Title);
            insertCommand.Parameters.AddWithValue("Author", bookList.Last().Author);
            insertCommand.Parameters.AddWithValue("Date", bookList.Last().Date);
            insertCommand.Parameters.AddWithValue("Rating", bookList.Last().Rating);
            insertCommand.Parameters.AddWithValue("Goal", bookList.Last().Goal);
            insertCommand.Parameters.AddWithValue("Genre", bookList.Last().Genre);
            insertCommand.Parameters.AddWithValue("Description", bookList.Last().Description);

            int rowsUpdated = insertCommand.ExecuteNonQuery();
            if (rowsUpdated == 1)
            {
                MessageBox.Show(bookList.Last().Title + " was inserted");
            }
            else
            {
                MessageBox.Show("insert failed");
            }
        }

        private void ReloadBooks()
        {
            //Clear bookListBox
            bookListBox.ClearSelected();

            //Open the database
            var connection = OpenConnection();

            //Create select command object
            var selectCommand = new SqlCommand("Select * from Book_Tbl;", connection);

            //Execute command into a DataReader
            var bookReader = selectCommand.ExecuteReader();

            if (bookReader.HasRows)
            {
                while (bookReader.Read())
                {
                    var storedBookObject = new Book();
                    storedBookObject.Title = bookReader["Title"].ToString();
                    storedBookObject.Author = bookReader["Author"].ToString();
                    storedBookObject.Date = DateTime.Parse(bookReader["Date"].ToString());
                    storedBookObject.Rating = (int)bookReader["Rating"];
                    storedBookObject.Goal = (int)bookReader["Goal"];
                    storedBookObject.Genre = bookReader["Genre"].ToString();
                    storedBookObject.Description = bookReader["Description"].ToString();

                    if (storedBookObject.BookID > bookLastNumber)
                    {
                        bookLastNumber = storedBookObject.BookID;
                    }
                    bookList.Add(storedBookObject);
                }
            }
            //Close the Connection
            connection.Close();
        }


        private void ClearLabels()
        {
            titleOutputLabel.Text = "";
            authorOutputLabel.Text = "";
            dateOutputLabel.Text = "";
            ratingOutputLabel.Text = "";
            goalOutputLabel.Text = "";
            genreOutputLabel.Text = "";
            descOutputLabel.Text = "";
        }
    }
}
