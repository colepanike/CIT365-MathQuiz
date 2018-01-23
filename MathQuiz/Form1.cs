﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        // Used to generate random values for the quiz
        private Random rand = new Random();

        private int add1;
        private int add2;
        private int sub1;
        private int sub2;
        private int multiplicand;
        private int multiplier;
        private int dividend;
        private decimal divisor;

        int timeLeft;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void StartTheQuiz()
        {
            // Addition
            add1 = rand.Next(51);
            add2 = rand.Next(51);
            // Subtraction
            sub1 = rand.Next(1, 101);
            sub2 = rand.Next(1, sub1);
            // Multiply
            multiplicand = rand.Next(2, 11);
            multiplier = rand.Next(2, 11);
            // Divide
            dividend = rand.Next(2, 11);
            int tempQuotient = rand.Next(2, 11);
            divisor = dividend * tempQuotient;

            // Put the values on display
            plusLeftLabel.Text = add1.ToString();
            plusRightLabel.Text = add2.ToString();
            minusLeftLabel.Text = sub1.ToString();
            minusRightLabel.Text = sub2.ToString();
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            divideLeftLabel.Text = dividend.ToString();
            divideRightLabel.Text = divisor.ToString();

            // Zero out the inputs
            sum.Value = difference.Value = product.Value = quotient.Value = 0;

            // Start the timer
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        /// <summary>
        /// Check the answer to see if the user got everything right.
        /// </summary>
        /// <returns>True if the answer's correct, false otherwise.</returns>
        private bool CheckAnswers()
        {
            if (add1 + add2 == sum.Value &&
                sub1 - sub2 == difference.Value &&
                multiplicand * multiplier == product.Value &&
                dividend / divisor == quotient.Value)
                return true;
            else
                return false;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckAnswers())
            {
                // If CheckTheAnswer() returns true, then the user 
                // got the answer right. Stop the timer  
                // and show a MessageBox.
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                if (timeLeft <= 6)
                {
                    timeLabel.ForeColor = Color.Red;
                }

                // Display the new time left
                // by updating the Time Left label.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
            }
            else
            {
                // If the user ran out of time, stop the timer, show
                // a MessageBox, and fill in the answers.
                timer1.Stop();
                timeLabel.ForeColor = Color.Black;
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");

                // Give the answers
                sum.Value = add1 + add2;
                difference.Value = sub1 - sub2;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;

                startButton.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the whole answer in the NumericUpDown control.
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
    }
}
