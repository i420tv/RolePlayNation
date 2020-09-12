# RAGE.MP UI Package
## Description
This is a graphical display, which serves to display the information fuel level and KM / H.
## Installation
1. move `speedometer` to `client_packages` folder.
3. add line in index.js in the `client_packages` folder with `require("/speedometer/script.js");`.
### IMPORTANT!
If you are using some client packages - don't override your `index.js` file in `client_packages` folder. Just add this line in `index.js`:
```JavaScript
require("/speedometer/script.js");
```
Otherwise another client packages wont work!

Send any Buggs at CommanderDonkey@gmail.com
or contact me at the ragemp forum https://rage.mp/profile/16781-commanderdonkey/

## License
Speedometer UI is released under the [MIT License](https://opensource.org/licenses/MIT).