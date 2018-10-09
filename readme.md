# Blazor.LibraryExample

This small example project is a simple proof of concept of the use of Blazor, including some mechanisms to improve accessibility in a SPA type application.
For more information, you can have a look at the Blazor documentation at: [https://blazor.net/docs/](https://blazor.net/docs/).

## Accessibility improvements implemented

### 1. Focusing and managing change detection

One of the most important problems of interaction with a SPA application and a screen reader is the difficulty of detecting what changes have occurred in the document, and where on the web we should focus our attention.
To solve this, I have created a focusing mechanism, which allows to focus any element of the web after rendering. Thus, each time you navigate to a new page, the focus will be placed in the header level 2, which starts the section of that page.

### 2. Links without href  attribute and use of aria role

I've modified some link roles without an href attribute (because introducing an href with "#" value Blazor didn't perform the action correctly), and when removing it, some screen readers didn't detect the a tag as a link. Therefore, the only way to get a good readability with screen readers, has been to add the attribute role="link" in the "a" tag which has not href attribute.

### 3. Hiding texts using CSS

Screen readers have mechanisms to obtain lists of almost anything, including links. In a results table, links whose text is only: "Edit", "Delete"... don't make sense in a decontextualized list. That's why links need to have a context of their own. To avoid problems with the design and very long texts, one of the most used techniques is to hide texts using CSS. The scren reader user can read the entire text (edit Everything is eventual, for example), but an unblind user will only see the visible part of the link, edit, in this case.

### 4. Adjusting the title according to the current page

It is essential that the title of the page (the tag "title" of the html header), contains the real value of the page in which we are, from the most particular to the most general. So, if we are at home > library > news, a valid title could be: News - library - Blazor Library Example. I used the Javascript interoperability feature to change the title of the page every time you navigate to a different page.
## Code quality

I'm a total rookie with Blazor, so I'm sure I've made a lot of errors in the code. Any corrections or improvements are welcome, off course.

Thank you all very much, and I hope you find it interesting!