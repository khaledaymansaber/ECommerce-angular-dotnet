import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-bar',
  standalone: false,
  styleUrl: './nav-bar.scss',
  templateUrl: './nav-bar.html',
})
export class NavBar {
  isDropdownOpen = false;
  ToggleDropdown(){
    this.isDropdownOpen = !this.isDropdownOpen;
  }
}
