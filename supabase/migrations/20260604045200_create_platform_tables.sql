/*
  # Create Platform Tables: Data Dictionaries, Layouts, Menus

  ## Overview
  Sets up the platform navigation and dictionary entities.

  ## New Tables

  ### `platform_datas`
  Data dictionary / configuration store.
  - `id` (uuid, PK)
  - `tenant_id` (uuid, nullable) - Multi-tenant support
  - `name` (varchar 64) - Dictionary name, indexed
  - `code` (varchar 1024) - Unique code path
  - `display_name` (varchar 128)
  - `description` (varchar 1024, nullable)
  - `parent_id` (uuid, nullable, self-reference)
  - `is_static` (bool) - Whether this is a system-defined entry
  - Full audit + soft delete

  ### `platform_data_items`
  Items belonging to a data dictionary entry.
  - `id` (uuid, PK)
  - `data_id` (uuid, FK → platform_datas)
  - `name`, `display_name`, `default_value`, `description`
  - `allow_be_null` (bool), `is_static` (bool)
  - `value_type` (smallint) - ValueType enum
  - `order` (int) - Sort order
  - Full audit + soft delete

  ### `platform_layouts`
  UI framework layout definitions (extends Route fields).
  - `id` (uuid, PK)
  - `tenant_id` (uuid, nullable)
  - Route fields: path, name, display_name, description, redirect, framework
  - `data_id` (uuid, FK → platform_datas)
  - `extra_properties` (jsonb) - required_features, required_permissions
  - Full audit + soft delete

  ### `platform_menus`
  Navigation menu items (extends Route fields, tree structure).
  - `id` (uuid, PK)
  - `tenant_id` (uuid, nullable)
  - Route fields: path, name, display_name, description, redirect
  - `framework` (varchar 64)
  - `code` (varchar 19) - Tree position code
  - `component` (varchar 256)
  - `parent_id` (uuid, nullable, self-reference)
  - `layout_id` (uuid, FK → platform_layouts)
  - `is_public` (bool)
  - `extra_properties` (jsonb)
  - Full audit + soft delete

  ### `platform_role_menus`
  Role-to-menu assignments.
  - `id` (uuid, PK)
  - `tenant_id` (uuid, nullable)
  - `menu_id` (uuid, FK → platform_menus)
  - `role_name` (varchar 256)
  - `startup` (bool) - Default startup menu for this role
  - Audit fields

  ### `platform_user_menus`
  User-to-menu assignments.
  - `id` (uuid, PK)
  - `tenant_id` (uuid, nullable)
  - `menu_id` + `user_id` composite unique key
  - `startup` (bool)
  - Audit fields

  ### `platform_user_favorite_menus`
  User bookmarked/favorite menu items.
  - `id` (uuid, PK)
  - `tenant_id`, `menu_id`, `user_id`
  - `alias_name`, `color`, `framework`, `name`, `display_name`, `path`, `icon`
  - Audit fields

  ## Security
  - RLS enabled on all tables
  - Authenticated users can read non-deleted records
  - Authenticated users can manage records they created

  ## Notes
  - ValueType enum stored as smallint (0=Undefined, etc.)
  - extra_properties stores JSON for RequiredFeatures and RequiredPermissions
*/

-- Platform Datas (Data Dictionary)
CREATE TABLE IF NOT EXISTS platform_datas (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid,
  name varchar(64) NOT NULL,
  code varchar(1024) NOT NULL,
  display_name varchar(128) NOT NULL,
  description varchar(1024),
  parent_id uuid REFERENCES platform_datas(id),
  is_static boolean DEFAULT false,
  extra_properties jsonb DEFAULT '{}'::jsonb,
  creation_time timestamptz DEFAULT now(),
  creator_id uuid,
  last_modification_time timestamptz,
  last_modifier_id uuid,
  is_deleted boolean DEFAULT false,
  deletion_time timestamptz,
  deleter_id uuid
);

CREATE INDEX IF NOT EXISTS idx_platform_datas_name ON platform_datas(name);
CREATE INDEX IF NOT EXISTS idx_platform_datas_tenant_id ON platform_datas(tenant_id);
CREATE INDEX IF NOT EXISTS idx_platform_datas_parent_id ON platform_datas(parent_id);
CREATE INDEX IF NOT EXISTS idx_platform_datas_is_deleted ON platform_datas(is_deleted);

ALTER TABLE platform_datas ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Authenticated users can read platform_datas"
  ON platform_datas FOR SELECT
  TO authenticated
  USING (is_deleted = false);

CREATE POLICY "Authenticated users can insert platform_datas"
  ON platform_datas FOR INSERT
  TO authenticated
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can update platform_datas"
  ON platform_datas FOR UPDATE
  TO authenticated
  USING (auth.uid() IS NOT NULL)
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can delete platform_datas"
  ON platform_datas FOR DELETE
  TO authenticated
  USING (auth.uid() IS NOT NULL);


-- Platform Data Items
CREATE TABLE IF NOT EXISTS platform_data_items (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid,
  data_id uuid NOT NULL REFERENCES platform_datas(id),
  name varchar(64) NOT NULL,
  display_name varchar(128) NOT NULL,
  default_value text,
  description text,
  allow_be_null boolean DEFAULT true,
  is_static boolean DEFAULT false,
  value_type smallint DEFAULT 0,
  "order" integer DEFAULT 0,
  extra_properties jsonb DEFAULT '{}'::jsonb,
  creation_time timestamptz DEFAULT now(),
  creator_id uuid,
  last_modification_time timestamptz,
  last_modifier_id uuid,
  is_deleted boolean DEFAULT false,
  deletion_time timestamptz,
  deleter_id uuid
);

CREATE INDEX IF NOT EXISTS idx_platform_data_items_data_id ON platform_data_items(data_id);
CREATE INDEX IF NOT EXISTS idx_platform_data_items_is_deleted ON platform_data_items(is_deleted);

ALTER TABLE platform_data_items ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Authenticated users can read platform_data_items"
  ON platform_data_items FOR SELECT
  TO authenticated
  USING (is_deleted = false);

CREATE POLICY "Authenticated users can insert platform_data_items"
  ON platform_data_items FOR INSERT
  TO authenticated
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can update platform_data_items"
  ON platform_data_items FOR UPDATE
  TO authenticated
  USING (auth.uid() IS NOT NULL)
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can delete platform_data_items"
  ON platform_data_items FOR DELETE
  TO authenticated
  USING (auth.uid() IS NOT NULL);


-- Platform Layouts
CREATE TABLE IF NOT EXISTS platform_layouts (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid,
  path varchar(256) NOT NULL,
  name varchar(64) NOT NULL,
  display_name varchar(128) NOT NULL,
  description varchar(256),
  redirect varchar(256),
  framework varchar(64) NOT NULL,
  data_id uuid REFERENCES platform_datas(id),
  extra_properties jsonb DEFAULT '{}'::jsonb,
  creation_time timestamptz DEFAULT now(),
  creator_id uuid,
  last_modification_time timestamptz,
  last_modifier_id uuid,
  is_deleted boolean DEFAULT false,
  deletion_time timestamptz,
  deleter_id uuid
);

CREATE INDEX IF NOT EXISTS idx_platform_layouts_name ON platform_layouts(name);
CREATE INDEX IF NOT EXISTS idx_platform_layouts_tenant_id ON platform_layouts(tenant_id);
CREATE INDEX IF NOT EXISTS idx_platform_layouts_is_deleted ON platform_layouts(is_deleted);

ALTER TABLE platform_layouts ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Authenticated users can read platform_layouts"
  ON platform_layouts FOR SELECT
  TO authenticated
  USING (is_deleted = false);

CREATE POLICY "Authenticated users can insert platform_layouts"
  ON platform_layouts FOR INSERT
  TO authenticated
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can update platform_layouts"
  ON platform_layouts FOR UPDATE
  TO authenticated
  USING (auth.uid() IS NOT NULL)
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can delete platform_layouts"
  ON platform_layouts FOR DELETE
  TO authenticated
  USING (auth.uid() IS NOT NULL);


-- Platform Menus
CREATE TABLE IF NOT EXISTS platform_menus (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid,
  path varchar(256) NOT NULL,
  name varchar(64) NOT NULL,
  display_name varchar(128) NOT NULL,
  description varchar(256),
  redirect varchar(256),
  framework varchar(64) NOT NULL,
  code varchar(19) NOT NULL,
  component varchar(256) NOT NULL,
  parent_id uuid REFERENCES platform_menus(id),
  layout_id uuid REFERENCES platform_layouts(id),
  is_public boolean DEFAULT false,
  extra_properties jsonb DEFAULT '{}'::jsonb,
  creation_time timestamptz DEFAULT now(),
  creator_id uuid,
  last_modification_time timestamptz,
  last_modifier_id uuid,
  is_deleted boolean DEFAULT false,
  deletion_time timestamptz,
  deleter_id uuid
);

CREATE INDEX IF NOT EXISTS idx_platform_menus_name ON platform_menus(name);
CREATE INDEX IF NOT EXISTS idx_platform_menus_tenant_id ON platform_menus(tenant_id);
CREATE INDEX IF NOT EXISTS idx_platform_menus_parent_id ON platform_menus(parent_id);
CREATE INDEX IF NOT EXISTS idx_platform_menus_layout_id ON platform_menus(layout_id);
CREATE INDEX IF NOT EXISTS idx_platform_menus_code ON platform_menus(code);
CREATE INDEX IF NOT EXISTS idx_platform_menus_is_deleted ON platform_menus(is_deleted);

ALTER TABLE platform_menus ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Authenticated users can read platform_menus"
  ON platform_menus FOR SELECT
  TO authenticated
  USING (is_deleted = false);

CREATE POLICY "Authenticated users can insert platform_menus"
  ON platform_menus FOR INSERT
  TO authenticated
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can update platform_menus"
  ON platform_menus FOR UPDATE
  TO authenticated
  USING (auth.uid() IS NOT NULL)
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can delete platform_menus"
  ON platform_menus FOR DELETE
  TO authenticated
  USING (auth.uid() IS NOT NULL);


-- Platform Role Menus
CREATE TABLE IF NOT EXISTS platform_role_menus (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid,
  menu_id uuid NOT NULL REFERENCES platform_menus(id),
  role_name varchar(256) NOT NULL,
  startup boolean DEFAULT false,
  creation_time timestamptz DEFAULT now(),
  creator_id uuid,
  last_modification_time timestamptz,
  last_modifier_id uuid
);

CREATE INDEX IF NOT EXISTS idx_platform_role_menus_menu_id ON platform_role_menus(menu_id);
CREATE INDEX IF NOT EXISTS idx_platform_role_menus_role_name ON platform_role_menus(role_name);
CREATE UNIQUE INDEX IF NOT EXISTS idx_platform_role_menus_role_menu ON platform_role_menus(role_name, menu_id);

ALTER TABLE platform_role_menus ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Authenticated users can read platform_role_menus"
  ON platform_role_menus FOR SELECT
  TO authenticated
  USING (true);

CREATE POLICY "Authenticated users can insert platform_role_menus"
  ON platform_role_menus FOR INSERT
  TO authenticated
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can update platform_role_menus"
  ON platform_role_menus FOR UPDATE
  TO authenticated
  USING (auth.uid() IS NOT NULL)
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can delete platform_role_menus"
  ON platform_role_menus FOR DELETE
  TO authenticated
  USING (auth.uid() IS NOT NULL);


-- Platform User Menus
CREATE TABLE IF NOT EXISTS platform_user_menus (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid,
  menu_id uuid NOT NULL REFERENCES platform_menus(id),
  user_id uuid NOT NULL,
  startup boolean DEFAULT false,
  creation_time timestamptz DEFAULT now(),
  creator_id uuid,
  last_modification_time timestamptz,
  last_modifier_id uuid
);

CREATE UNIQUE INDEX IF NOT EXISTS idx_platform_user_menus_user_menu ON platform_user_menus(user_id, menu_id);
CREATE INDEX IF NOT EXISTS idx_platform_user_menus_user_id ON platform_user_menus(user_id);

ALTER TABLE platform_user_menus ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Users can read their own user menus"
  ON platform_user_menus FOR SELECT
  TO authenticated
  USING (auth.uid() = user_id);

CREATE POLICY "Users can insert their own user menus"
  ON platform_user_menus FOR INSERT
  TO authenticated
  WITH CHECK (auth.uid() = user_id);

CREATE POLICY "Users can update their own user menus"
  ON platform_user_menus FOR UPDATE
  TO authenticated
  USING (auth.uid() = user_id)
  WITH CHECK (auth.uid() = user_id);

CREATE POLICY "Users can delete their own user menus"
  ON platform_user_menus FOR DELETE
  TO authenticated
  USING (auth.uid() = user_id);


-- Platform User Favorite Menus
CREATE TABLE IF NOT EXISTS platform_user_favorite_menus (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid,
  menu_id uuid NOT NULL REFERENCES platform_menus(id),
  user_id uuid NOT NULL,
  alias_name varchar(128),
  color varchar(64),
  framework varchar(64) NOT NULL,
  name varchar(64) NOT NULL,
  display_name varchar(128) NOT NULL,
  path varchar(256) NOT NULL,
  icon varchar(512),
  creation_time timestamptz DEFAULT now(),
  creator_id uuid,
  last_modification_time timestamptz,
  last_modifier_id uuid
);

CREATE INDEX IF NOT EXISTS idx_platform_user_fav_menus_user_id ON platform_user_favorite_menus(user_id);
CREATE INDEX IF NOT EXISTS idx_platform_user_fav_menus_menu_id ON platform_user_favorite_menus(menu_id);

ALTER TABLE platform_user_favorite_menus ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Users can read their own favorite menus"
  ON platform_user_favorite_menus FOR SELECT
  TO authenticated
  USING (auth.uid() = user_id);

CREATE POLICY "Users can insert their own favorite menus"
  ON platform_user_favorite_menus FOR INSERT
  TO authenticated
  WITH CHECK (auth.uid() = user_id);

CREATE POLICY "Users can update their own favorite menus"
  ON platform_user_favorite_menus FOR UPDATE
  TO authenticated
  USING (auth.uid() = user_id)
  WITH CHECK (auth.uid() = user_id);

CREATE POLICY "Users can delete their own favorite menus"
  ON platform_user_favorite_menus FOR DELETE
  TO authenticated
  USING (auth.uid() = user_id);
