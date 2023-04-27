# CurrencyInWordsApp
currency into words converter as server-client app

## Solution Structure
- **CIWClientApp** user interface
- **CIWConverter** conversion logic
- **CIWConverterTest** test project for conversion logic
- **CIWServerApi** conversion api service

## Usage
- make sure solution has multiple project start configured
  - ideally *CIWServerApi* should start first
- start and wait until both, *CIWServerApi* and *CIWClientApp* are running
- type the amount
  - heed the required format 
  - only digits and optionally one comma are allowed (d[[,d]d])
  - whitespaces are ignored
- press button *Convert* to turn numerical value into words
- check *Convert On Typing* to convert on the fly while typing
- press button *Copy Result to Clipboard* to copy result string to clipboard

## License
MIT License

Copyright (c) 2023 Robert Borgmann

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
