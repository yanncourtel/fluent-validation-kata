# Fluent Validation Kata

Kata to start with [FluentValidation](https://docs.fluentvalidation.net/) library.

## Packages

- [FluentAssertions](https://fluentassertions.com/)
- [FluentValidation](https://fluentvalidation.net/)
- [Xunit](https://xunit.net/)

## Your task

Implement a validator for the Product class satisfying the following rules.

## Validation Rules

### Product

- A mandatory property is neither null nor empty nor blank 
- Properties Reference, Language, Gtin, SellerId and CategoryId are mandatory
- The Language is [ISO 639-1](https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes) compliant (2 chars string, lower case)
- The Gtin allows GTIN-8, GTIN-12, GTIN-13 and GTIN-14 formats
- If provided, the Description is neither empty nor blank
- If provided, there must be between 2 and 5 pictures
- The CategoryId is a sequence of 2, 4 or 6 uppercase letters

## Need some help ?

### Strategy
- Create extension methods to group common validation patterns
- Create custom validators for complex property validation

### GTIN Check Digit Calculator

<table>
	<tr>
		<th>Format</th>
		<th colspan="14">Digit position</th>
	</tr>
	<tr>
		<th>GTIN-8</th>
		<td colspan="6"></td><td>N1</td><td>N2</td>
		<td>N3</td><td>N4</td><td>N5</td><td>N6</td>
		<td>N7</td><td>N8</td>
	</tr>
	<tr>
		<th>GTIN-12</th>
		<td colspan="2"></td><td>N1</td><td>N2</td>
		<td>N3</td><td>N4</td><td>N5</td><td>N6</td>
		<td>N7</td><td>N8</td><td>N9</td><td>N10</td>
		<td>N11</td><td>N12</td>
	</tr>
	<tr>
		<th>GTIN-13</th>
		<td></td><td>N1</td><td>N2</td>
		<td>N3</td><td>N4</td><td>N5</td><td>N6</td>
		<td>N7</td><td>N8</td><td>N9</td><td>N10</td>
		<td>N11</td><td>N12</td><td>N13</td>
	</tr>
	<tr>
		<th>GTIN-14</th>
		<td>N1</td><td>N2</td>
		<td>N3</td><td>N4</td><td>N5</td><td>N6</td>
		<td>N7</td><td>N8</td><td>N9</td><td>N10</td>
		<td>N11</td><td>N12</td><td>N13</td><td>N14</td>
	</tr>
	<tr>
		<th>Step 1</th>
		<td colspan="14">
			Multiply value of each position by :
		</td>
	</tr>
	<tr>
		<th></th>
		<td>x3</td><td>x1</td><td>x3</td><td>x1</td><td>x3</td><td>x1</td>
		<td>x3</td><td>x1</td><td>x3</td><td>x1</td><td>x3</td><td>x1</td>
		<td>x3</td><td>x0</td>
	</tr>
	<tr>
		<th>Step 2</th>
		<td colspan="14">
			Add results together to create <strong>sum</strong>
		</td>
	</tr>
	<tr>
		<th>Step 3</th>
		<td colspan="14">
			Subtract the sum from the nearest 
			equal or higher multiple of ten = <strong>CHECK DIGIT</strong>
		</td>
	</tr>
</table>

See https://www.gs1us.org/tools/check-digit-calculator for more details