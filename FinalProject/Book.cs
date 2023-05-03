//Author: Ali AL-Habarneh
//Date: 03/12/2023
//Purpose: Book class of tracker program
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Book
    {
        private int mBookID;
        private string mTitle;
        private string mAuthor;
        private DateTime mDate;
        private string mDescription;
        private int mGoal;
        private int mRating;
        private string mGenre;


        public int BookID { get { return mBookID; } set { mBookID = value; } }

        public string Title { get { return mTitle; } set { mTitle = value; } }

        public string Author { get { return mAuthor;} set { mAuthor = value; } }

        public DateTime Date { get { return mDate; } set { mDate = value;} }

        public string Description { get { return mDescription; } set { mDescription = value; } }

        public int Goal { get { return mGoal; } set { mGoal = value; } }

        public int Rating { get { return mRating; } set { mRating = value; } }

        public string Genre { get { return mGenre; } set { mGenre = value; } }


        public Book() 
        {
            this.mBookID = 0;
            this.mTitle = String.Empty;
            this.mAuthor = String.Empty;
            this.mDate = DateTime.Now;
            this.mRating = 0;
            this.mGoal = 0;
            this.mGenre = String.Empty;
            this.mDescription = String.Empty;
        }

        public Book(int ID)
        {
            this.mBookID = ID;
            this.mTitle = String.Empty;
            this.mAuthor = String.Empty;
            this.mDate = DateTime.Now;
            this.mRating = 0;
            this.mGoal = 0;
            this.mGenre = String.Empty;
            this.mDescription = String.Empty;
        }
    }
}
