let num1 = 5;
let num2 = 10;
let helper = 0;

console.log('Previous: ' + num1 + " and " + num2)

helper = num1;
num1 = num2;
num2 = helper;

console.log('Current: ' + num1 + " and " + num2)