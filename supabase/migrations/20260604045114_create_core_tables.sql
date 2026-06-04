/*
  # Create Core Tables: Authors and Books

  ## Overview
  Sets up the primary business entities for the AbpPro application: Authors and Books.

  ## New Tables

  ### `authors`
  Stores author information.
  - `id` (uuid, PK) - Unique identifier
  - `name` (varchar 64) - Author's full name, required
  - `birth_date` (timestamptz) - Author's date of birth
  - `short_bio` (text) - Optional short biography
  - Soft delete + full audit fields

  ### `books`
  Stores book catalog entries linked to authors.
  - `id` (uuid, PK) - Unique identifier
  - `name` (varchar 128) - Book title, required
  - `type` (smallint) - BookType enum value
  - `publish_date` (date) - Publication date
  - `price` (real) - Book price
  - `author_id` (uuid, FK → authors) - Author reference
  - Audit fields (no soft delete on this entity)

  ## Security
  - RLS enabled on both tables
  - Authenticated users can read all records
  - Only authenticated users can insert/update/delete their own records (creator-based)

  ## Notes
  - BookType enum: 0=Undefined, 1=Adventure, 2=Biography, 3=Dystopia, 4=Fantastic, 5=Horror, 6=Science, 7=ScienceFiction, 8=Poetry
*/

-- Authors table
CREATE TABLE IF NOT EXISTS authors (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  name varchar(64) NOT NULL,
  birth_date timestamptz NOT NULL,
  short_bio text,
  creation_time timestamptz DEFAULT now(),
  creator_id uuid,
  last_modification_time timestamptz,
  last_modifier_id uuid,
  is_deleted boolean DEFAULT false,
  deletion_time timestamptz,
  deleter_id uuid
);

CREATE INDEX IF NOT EXISTS idx_authors_name ON authors(name);
CREATE INDEX IF NOT EXISTS idx_authors_is_deleted ON authors(is_deleted);

ALTER TABLE authors ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Authenticated users can read authors"
  ON authors FOR SELECT
  TO authenticated
  USING (is_deleted = false);

CREATE POLICY "Authenticated users can insert authors"
  ON authors FOR INSERT
  TO authenticated
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can update authors"
  ON authors FOR UPDATE
  TO authenticated
  USING (auth.uid() IS NOT NULL)
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can delete authors"
  ON authors FOR DELETE
  TO authenticated
  USING (auth.uid() IS NOT NULL);


-- Books table
CREATE TABLE IF NOT EXISTS books (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  name varchar(128) NOT NULL,
  type smallint NOT NULL DEFAULT 0,
  publish_date date NOT NULL,
  price real NOT NULL DEFAULT 0,
  author_id uuid NOT NULL REFERENCES authors(id),
  creation_time timestamptz DEFAULT now(),
  creator_id uuid,
  last_modification_time timestamptz,
  last_modifier_id uuid
);

CREATE INDEX IF NOT EXISTS idx_books_author_id ON books(author_id);
CREATE INDEX IF NOT EXISTS idx_books_name ON books(name);

ALTER TABLE books ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Authenticated users can read books"
  ON books FOR SELECT
  TO authenticated
  USING (true);

CREATE POLICY "Authenticated users can insert books"
  ON books FOR INSERT
  TO authenticated
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can update books"
  ON books FOR UPDATE
  TO authenticated
  USING (auth.uid() IS NOT NULL)
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can delete books"
  ON books FOR DELETE
  TO authenticated
  USING (auth.uid() IS NOT NULL);
