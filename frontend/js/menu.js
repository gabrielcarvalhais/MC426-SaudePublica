class Menu {
  constructor(containerId) {
    this.containerId = containerId;
  }

  render() {
    const menuContainer = document.getElementById(this.containerId);
    addEventListener("load", () => {
      menuContainer.innerHTML = "<object type='text/html' data='../menu.html' ></object>";
    });
  }
}
